﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.Common
{
    [Serializable]
    public abstract class ValidationBase : BindableBase
    {
        public ValidationErrors ValidationErrors { get; set; }
        [XmlIgnore]
        public bool IsValid { get; private set; }

        protected ValidationBase()
        {
            this.ValidationErrors = new ValidationErrors();
        }

        protected abstract void ValidateSelf();

        public void Validate()
        {
            this.ValidationErrors.Clear();
            this.ValidateSelf();
            this.IsValid = this.ValidationErrors.IsValid;
            this.OnPropertyChanged("IsValid");
            this.OnPropertyChanged("ValidationErrors");
        }
    }
}
