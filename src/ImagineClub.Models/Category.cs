namespace ImagineClub.Web.Models
{
    using System.Linq;

    public class Category
    {
        public static Category Student = new Category("Student(in)");
        public static Category Schooolboy = new Category("Schüler(in)");
        public static Category Alumni = new Category("Alumi");
        public static Category Others = new Category("Sonstige");
        public static Category Scientific = new Category("Wissenschaftliche(r) Beirat");

        public static Category[] All = new[]{Student, Schooolboy, Alumni, Others, Scientific};

        public string Name
        {
            get; protected set;
        }

        private Category(string categoryName)
        {
            Name = categoryName;
        }

        public static Category GetCategoryByName(string name)
        {
            return All.Single(p => p.Name == name);
        }

        public bool Equals(Category other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Category)) return false;
            return Equals((Category) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}