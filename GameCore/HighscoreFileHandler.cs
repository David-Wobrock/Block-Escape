using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class HighscoreFileHandler
    {
        private HighscoreFileHandler()
        {}

        private static HighscoreFileHandler instance = null;

        public static HighscoreFileHandler getInstance()
        {
            if (instance == null)
                instance = new HighscoreFileHandler();
            return instance;
        }

        /// <summary>
        /// The name of the highscore file
        /// </summary>
        private String FileName = "BlockEscape_Highscore.txt";

        /// <summary>
        /// Loads the highscore
        /// </summary>
        /// <returns>The highscore or -1 if no file</returns>
        public int LoadHighscore()
        {
            try
            {
                String fileContent = File.ReadAllText(FileName);
                int highscore = Int32.Parse(fileContent);
                return highscore;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Saves the highscore in the file
        /// </summary>
        /// <param name="highscore">The highscore to save</param>
        public void SaveHighscore(int highscore)
        {
            String fileContent = highscore.ToString();
            File.WriteAllText(FileName, fileContent);
        }
    }
}
