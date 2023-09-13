using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    class RenPlayer
    {
        private bool isPlaying = false;
        private StringBuilder returnData; 
        public bool IsPlaying { get { return isPlaying;} }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLenght, IntPtr hwdCallBack);
        
        public void Open(string File)
        {
            string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
            string command = string.Format(Format, File);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public void Play()
        {
            string command = "play MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);
            isPlaying = true;
        }

        public void Stop()
        {
            string command = "stop MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);
            isPlaying = false;
        }


        /*      this is code that isn't finished
         *      
         *      
         *      public int GetCurrentMilisecond()
            {
                string command = "status MediaFile position";
                mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
                return int.Parse(returnData.ToString());
            }

            public int GetSongLenght()
            {
                if (IsPlaying)
                {
                    string command = "status MediaFile length";
                    mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
                    return int.Parse(returnData.ToString());
                }
                else
                    return 0;
            }
        */

    }
}
