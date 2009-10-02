namespace ImagineClub.Web.Models
{
    using System.Linq;

    public class Salutation
    {
        private readonly string _name;
        public static Salutation Mr = new Salutation("Herr");
        public static Salutation Mrs = new Salutation("Frau");

        public static Salutation[] All = new[] {Mr, Mrs};

        public Salutation(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public static Salutation GetByName(string name)
        {
            return All.Single(p => p.Name == name);
        }

        public bool Equals(Salutation other)
        {
            return !ReferenceEquals(null, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Salutation)) return false;
            return Equals((Salutation) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(Salutation left, Salutation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Salutation left, Salutation right)
        {
            return !Equals(left, right);
        }
    }
}