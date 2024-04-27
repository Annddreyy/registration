using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHelper = Microsoft.Office.Interop.Word;

namespace Регистрация_Пациентов
{
    internal class Word
    {
        private FileInfo _fileInfo;

        public Word(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new Exception();
            }
        }

        internal bool Process(Dictionary<string, string> dic)
        {
            WordHelper.Application app = null;
            try
            {
                app = new WordHelper.Application();

                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;

                app.Documents.Open(file);

                foreach (var item in dic)
                {
                    WordHelper.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = WordHelper.WdFindWrap.wdFindContinue;
                    Object replace = WordHelper.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing,
                        Replace: replace);
                }
                Object newFileName = Path.Combine(_fileInfo.DirectoryName, "Заполненное " + _fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (app != null)
                    app.Quit();
            }
            return false;
        }
    }
}
