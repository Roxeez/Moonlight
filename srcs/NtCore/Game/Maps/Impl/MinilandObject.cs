namespace NtCore.Game.Maps
{
    public class MinilandObject : IMinilandObject
    {
        public int Vnum { get; set; }
        public int Id { get; set; }
        public Position Position { get; set; }

        protected bool Equals(MinilandObject other) => Vnum == other.Vnum && Id == other.Id && Position.Equals(other.Position);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((MinilandObject)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Vnum;
                hashCode = hashCode * 397 ^ Id;
                hashCode = hashCode * 397 ^ Position.GetHashCode();
                return hashCode;
            }
        }
    }
}