using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ErrorDto
    {
        public IList<string> Errors { get; private set; }
        public bool IsShow { get; private set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }
        public ErrorDto(string errors, bool isShow)
        {
            Errors.Add(errors);
            IsShow = isShow;
        }
        public ErrorDto(IList<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
