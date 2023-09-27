using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachograph
{
    internal class FileManager
    {
        string socketEditorfilePath;
        string readingOutputfilePath;
        string projectDirectory;
        public string SocketEditorFileName { get { return socketEditorFileName; } }
        string socketEditorFileName = "socket_editor.txt";
        public string ReadingOutputFileName { get { return readingOutputFileName; } }
        string readingOutputFileName = "reading_output.txt";
        StreamWriter writer;

        public FileManager()
        {
            SetupFiles();
        }

        /// <summary>
        /// Vytvoří (pokud již neexistují) soubory pro zápis z vyčítání a také soubor na spravování IP adres
        /// </summary>
        void SetupFiles()
        {
            // Získejte cestu k adresáři projektu, kde chcete vytvořit soubor "socket_editor.txt"
            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // Sestavte úplnou cestu k souboru "socket_editor.txt"
            socketEditorfilePath = Path.Combine(projectDirectory, socketEditorFileName);
            readingOutputfilePath = Path.Combine(projectDirectory, readingOutputFileName);
            // Zkontrolujte, zda soubor již existuje
            if (!File.Exists(socketEditorfilePath))         
                File.Create(socketEditorfilePath).Close();
            if (!File.Exists(readingOutputfilePath))
                File.Create(readingOutputfilePath).Close();
        }

        public void OpenWriterForReadingOutput()
        {
            writer = new StreamWriter(readingOutputfilePath);
        }

        public void CloseWriter()
        {
            writer.Close();
        }

        /// <summary>
        /// Metoda na tisknutí bytů přijatých dat (podoba hexdumpu)
        /// </summary>
        /// <param name="data"> Obdržená data v packetu </param>
        public void PacketOutput(byte[] data, int packetIndex)
        {
            int i = 0;
            int rowWidth = 16;

            foreach (byte b in data)
            {
                if (i % rowWidth == 0) // vypisuje vždy po konkrétním počtu bytů (standardně po 16, jako hexdump)
                {
                    writer.Write("\n");
                    i = 0;
                }
                writer.Write(b.ToString("X2") + " "); // output je v šestnáctkové soustavě
                i++;
            }
        }
    }
}
