using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSEnterprise
{
    class Error
    {
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private ErrorType errorType;
        public ErrorType ErrorType
        {
            get { return errorType; }
            set { errorType = value; }
        }

        public Error(int index, string description, string type, ErrorType errorType, string id)
        {
            
            this.Index = index;
            this.Description = description;
            this.Type = type;
            this.ErrorType = errorType;
            this.Id = id;
        }
    }
}
