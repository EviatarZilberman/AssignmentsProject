using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogWriter
    {
        private static string PATH = @"C:\Logs\System.log";
        private string NewPath { get; set; } = string.Empty;
        private StreamWriter StreamWriter { get; set; }
        private static LogWriter LoggerWriter { get; set; } = null;


        private LogWriter(string newPathINPUT)
        {
            if (string.IsNullOrEmpty(newPathINPUT))
            {
                NewPath = PATH;
            }
            else
            {
                NewPath = newPathINPUT;
            }
            StreamWriter = new StreamWriter(NewPath, true);
            //StreamWriter.Flush();
        }

        private LogWriter()
        {
            NewPath = PATH;
            StreamWriter = new StreamWriter(NewPath, true);
            //StreamWriter.Flush();

        }

        public static LogWriter Instance()
        {
            if (LoggerWriter == null)
            {
                LoggerWriter = new LogWriter();
            }
            return LoggerWriter;
        }

        public static LogWriter Instance(string newPathINPUT)
        {
            if (LoggerWriter == null)
            {
                LoggerWriter = new LogWriter(newPathINPUT);
            }
            return LoggerWriter;
        }

        private static CoreReturns CreateFile(string newPathINPUT)
        {
            if (!string.IsNullOrEmpty(newPathINPUT))
            {
                return CoreReturns.PATH_IS_INVALID;
            }
            else
            {
                if (!File.Exists(newPathINPUT))
                {
                    File.Create(newPathINPUT).Close();
                }
                else
                {
                    return CoreReturns.FILE_EXISTS;
                }
            }
            return CoreReturns.SUCCESS;
        }

        private static CoreReturns CreateFile()
        {
            if (!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }

            return CoreReturns.SUCCESS;
        }

        public CoreReturns WriteLog(string callFuncName, string messageINPUT, string newPathINPUT)
        {
            if (CreateFile(newPathINPUT) == CoreReturns.SUCCESS || CreateFile(newPathINPUT) == CoreReturns.FILE_EXISTS)
            {
                Instance(newPathINPUT).StreamWriter.WriteLine
                    ($"[{DateTime.Now}:{DateTime.Now.Millisecond}] [{callFuncName}] => {messageINPUT}", true);
                //  Logger.Instance().StreamWriter.Close();

            }
            else
            {
                CreateFile(newPathINPUT);
                Instance(newPathINPUT).StreamWriter.WriteLine
                  ($"[{DateTime.Now}:{DateTime.Now.Millisecond}] [{callFuncName}] => {messageINPUT}", true);
                //             Logger.Instance().StreamWriter.Close();

            }
            Instance().StreamWriter.Flush();

            return CoreReturns.SUCCESS;
        }

        public CoreReturns WriteLog(string callFuncName, string messageINPUT)
        {
            if (CreateFile() == CoreReturns.SUCCESS || CreateFile() == CoreReturns.FILE_EXISTS)
            {
                Instance().StreamWriter.WriteLine
                    ($"[{DateTime.Now}:{DateTime.Now.Millisecond}] [{callFuncName}] => {messageINPUT}", true);
            }
            else
            {
                CreateFile();
                Instance().StreamWriter.WriteLine
                    ($"[{DateTime.Now}:{DateTime.Now.Millisecond}] [{callFuncName}] => {messageINPUT}", true);
            }

            Instance().StreamWriter.Flush();

            return CoreReturns.SUCCESS;
        }
    }
}
