namespace DartSharp.Commands
{
    public class NullCommand : ICommand
    {
        private static NullCommand instance = new NullCommand();

        private NullCommand()
        {
        }

        public static NullCommand Instance { get { return instance; } }

        public object Execute(Context context)
        {
            return null;
        }
    }
}
