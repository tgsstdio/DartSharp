namespace DartSharp.Language
{
    public class Predicates
    {
        public static bool IsFalse(object obj)
        {
            return !IsTrue(obj);
        }

        public static bool IsTrue(object obj)
        {
            if (obj == null)
                return false;

            if (obj is bool)
                return (bool)obj == true;

            return false;
        }
    }
}

