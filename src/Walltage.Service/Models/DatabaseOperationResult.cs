using System.Collections.Generic;

namespace Walltage.Service.Models
{
    public class DatabaseOperationResult
    {
        public DatabaseOperationResult()
        {
            this.Errors = new List<string>(5);
        }

        public bool Success 
        {
            get { return this.Errors.Count == 0; } 
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        public IList<string> Errors { get; set; }
    }
}
