using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Mg.Challenge.Core.Services;

namespace Mg.Challenge.Core.Builders
{
    public abstract class SequentialDtoBuilder<TDto>
        where TDto : new()
    {
        protected Dictionary<int, Action<TDto, string>> _propertyMappers = new Dictionary<int, Action<TDto, string>>();
        private string _recordType;

        public SequentialDtoBuilder(string recordType)
        {
            _recordType = recordType;
        }

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

        public TDto Build(string[] inputs)
        {
            var splitLine = inputs[0].Trim().Split(',');

            // We only want to process our lines.
            if(splitLine[0].Clean() == _recordType)
            {
                var dto = new TDto();

                for(int i = 1; i < splitLine.Length; i++)
                {
                    if (_propertyMappers.ContainsKey(i))
                        _propertyMappers[i](dto, splitLine[i].Clean());
                }

                return dto;
            }

            return default(TDto);
        }
    }
}
