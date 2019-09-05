using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Mg.Challenge.Core.Services;

namespace Mg.Challenge.Core.Builders
{
    /// <summary>
    /// This is the base class for how all of the mapping logic will get handled.  Child classes are expected to be 
    /// scoped to a particular record type and should define their mappings in their constructors.
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public abstract class SequentialDtoBuilder<TDto>
        where TDto : new()
    {
        // This the dictionary that holds the mappings of offsets to properties.  The key is the offset in the row of the CSV
        // and the value is a function that will assign the string to the correct property.
        protected Dictionary<int, Action<TDto, string>> _propertyMappers = new Dictionary<int, Action<TDto, string>>();
        // This will store processors that any properties on the DTO will need to hydrate them.
        protected Dictionary<string, Func<TDto, string[], int, int>> _handlers = new Dictionary<string, Func<TDto, string[], int, int>>();
        private string _recordType;

        public SequentialDtoBuilder(string recordType)
        {
            _recordType = recordType;
        }

        // The type of record this builder can process.
        public string RecordType => _recordType;

        protected void RegisterMapping(int offset, Action<TDto, int> setter)
        {
            if (!_propertyMappers.ContainsKey(offset))
            {
                _propertyMappers.Add(offset, (dto, value) =>
                {
                    // Not really sure how to handle invalid data, so just ignoring and doing best attempt at the rest
                    // of the file.
                    if (int.TryParse(value, out int parsedValue))
                        setter(dto, parsedValue);
                });
            }
            else
                throw new ArgumentException($"Offset {offset} has already been registered for {typeof(TDto).Name}");
        }

        protected void RegisterMapping(int offset, Action<TDto, DateTime> setter)
        {
            if (!_propertyMappers.ContainsKey(offset))
            {
                _propertyMappers.Add(offset, (dto, value) =>
                {
                    // Not really sure how to handle invalid data, so just ignoring and doing best attempt at the rest
                    // of the file.
                    if (DateTime.TryParse(value, out DateTime parsedValue))
                        setter(dto, parsedValue);
                });
            }
            else
                throw new ArgumentException($"Offset {offset} has already been registered for {typeof(TDto).Name}");
        }

        protected void RegisterMapping(int offset, Action<TDto, string> setter)
        {
            if (!_propertyMappers.ContainsKey(offset))
            {
                _propertyMappers.Add(offset, (dto, value) => setter(dto, value));
            }
            else
                throw new ArgumentException($"Offset {offset} has already been registered for {typeof(TDto).Name}");
        }

        /// <summary>
        /// This the way to register other SequentialDtoBuilder's and have them processed recursively.
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="builder">The DtoBuilder for this property.</param>
        /// <param name="setter">An action that will be used to add the result of the recursive builder to the DTO.</param>
        protected void RegisterChildDto<TProp>(SequentialDtoBuilder<TProp> builder, Action<TDto, TProp> setter) where TProp : new()
        {
            if (!_handlers.ContainsKey(builder.RecordType))
            {
                _handlers.Add(builder.RecordType, (dto, strings, start) =>
                {
                    // We call the child builder and get the result.
                    var child = builder.Build(strings, start);

                    // Invoke the action with the child DTO.
                    setter(dto, child.Item1);

                    // Return out the child's row number so that we know where to resume processing.  If the child DTO
                    // has a rich object graph, it may have processed a number of rows and we don't want to reprocess them.
                    return child.Item2;
                });
            }
        }

        /// <summary>
        /// Builds the DTO from the inputs.
        /// </summary>
        /// <param name="inputs">The lines representing the CSV file.</param>
        /// <param name="startingLine">Which line to start processing at.  This allows it be called recursively.</param>
        /// <returns>A Tuple where Item1 is the dto and Item2 is index of the last row processed.</returns>
        public (TDto, int) Build(string[] inputs, int startingLine = 0)
        {
            var splitLine = inputs[startingLine].Trim().Split(',');

            // We only want to process our lines.
            if(splitLine[0].Clean() == _recordType)
            {
                var dto = new TDto();

                // This for loop will handle populating the primitive properties on the DTO by looping through the
                // columns in the CSV row and looking up any mappings that were defined. 
                for(int i = 1; i < splitLine.Length; i++)
                {
                    if (_propertyMappers.ContainsKey(i))
                        _propertyMappers[i](dto, splitLine[i].Clean());
                }

                int recordOffset = startingLine + 1;

                // Once the primitive properties are taken care of we'll try to take care of other DTOs that may exist
                // in the object graph.
                while (recordOffset < inputs.Length)
                {
                    var split = inputs[recordOffset].Split(',');
                    var recordType = split[0].Clean();

                    // We check to see if the next row is one that represents one of our direct children.  If it isn't
                    // we return early because we've clearly moved on into a separate part of the object graph.
                    if (!_handlers.ContainsKey(recordType))
                        return (dto, recordOffset);
                    else
                        // We have a handler for this record type, which means we need to process it since it will be
                        // used to build up one of our child DTOs.  This will indirectly call another builder and that
                        // builder will process some number of rows, so we update the offset here to avoid reprocessing rows.
                        recordOffset = _handlers[recordType](dto, inputs, recordOffset);
                }

                return (dto, inputs.Length);
            }

            return (default(TDto), 0);
        }
    }
}
