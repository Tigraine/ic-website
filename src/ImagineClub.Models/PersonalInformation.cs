namespace ImagineClub.Web.Models
{
    using System;
    using Castle.ActiveRecord;
    using Castle.Components.Validator;

    public class PersonalInformation
    {
        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Firstname { get; set; }

        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Lastname { get; set; }

        [Property]
        public string BirthPlace { get; set; }
        
        [Property(NotNull = true)]
        public string Nationality { get; set; }
        
        [Property]
        public string MatrNr { get; set; }

        [ValidateDateTime]
        [Property]
        public DateTime? BirthDay { get; set; }

        [Field(NotNull = true)]
        private string _salutation;

        public Salutation Salutation
        {
            get { return Salutation.GetByName(_salutation); }
            set { _salutation = value.Name; }
        }

        [Property]
        public string Title { get; set; }

        [Field]
        private string _category;
        public Category Category
        {
            get { return Category.GetCategoryByName(_category); }
            set { _category = value.Name; }
        }
    }
}