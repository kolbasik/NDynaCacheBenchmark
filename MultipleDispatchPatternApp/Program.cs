using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDispatchPatternApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IExceptionHandler defaultHandler = new FileSystemExceptionHandler();
            defaultHandler.HandleException(new IOException()); // Handle(IOException) overload
            defaultHandler.HandleException(new DirectoryNotFoundException()); // Handle(IOException) overload
            defaultHandler.HandleException(new FileNotFoundException()); // Handle(FileNotFoundException) overload
            defaultHandler.HandleException(new FormatException()); // Handle(Exception) => OnFallback

            Console.ReadKey(true);
        }
    }

    public interface IExceptionHandler
    {
        void HandleException<T>(T exception) where T : Exception;
    }

    public interface IExceptionHandler<in T> where T : Exception
    {
        void Handle(T exception);
    }

    public class FileSystemExceptionHandler : IExceptionHandler,
    //IExceptionHandler<Exception>,
    IExceptionHandler<IOException>,
    IExceptionHandler<FileNotFoundException>
    {
        public void HandleException<T>(T exception) where T : Exception
        {
            var handler = this as IExceptionHandler<T>;
            if (handler != null)
                handler.Handle(exception);
            else
                this.Handle(exception);
        }

        public void Handle(Exception exception)
        {
            OnFallback(exception);
        }

        protected virtual void OnFallback(Exception exception)
        {
            // rest of implementation
            Console.WriteLine("Fallback: {0}", exception.GetType().Name);
        }

        public void Handle(IOException exception)
        {
            // rest of implementation
            Console.WriteLine("IO spec");
        }

        public void Handle(FileNotFoundException exception)
        {
            // rest of implementation
            Console.WriteLine("FileNotFoundException spec");
        }
    }
}
