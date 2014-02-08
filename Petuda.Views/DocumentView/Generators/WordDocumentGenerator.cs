using System.IO;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace Petuda.Views.DocumentView.Generators
{
    public class WordDocumentGenerator
    {
        private Word.Application wordApp;
        private Word.Document wordDoc;
        /* \endofdoc is a predefined bookmark */
        private object oEndOfDoc = "\\endofdoc"; 
        private string documentTitle;

        private static string _directoryName = System.AppDomain.CurrentDomain.BaseDirectory + "Docs\\";

        public WordDocumentGenerator(string title)
        {
            wordApp = new Application();
            wordDoc = wordApp.Documents.Add();
            wordApp.Visible = true;
            documentTitle = title;
            AddTitle(documentTitle);
        }

        public void SaveDocument()
        {
            if (!Directory.Exists(_directoryName))
            {
                Directory.CreateDirectory(_directoryName);
            }

            wordDoc.SaveAs(string.Format("{0}{1}.doc",_directoryName, CorrectFileName(documentTitle)));
        }
        

        private void AddTitle(string title)
        {
            //Insert a paragraph at the beginning of the document.
            Word.Paragraph titlePara;
            titlePara = wordDoc.Content.Paragraphs.Add();

            titlePara.Range.Text = title; 
            titlePara.Range.Font.Name = "Times New Roman";
            titlePara.Range.Font.Size = 15;
            titlePara.Range.Font.Bold = 1;

            titlePara.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            titlePara.Format.SpaceAfter = 12;    //12 pt spacing after paragraph.
            titlePara.Range.InsertParagraphAfter();

            titlePara.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            titlePara.Range.Font.Bold = 0;
            titlePara.Format.SpaceAfter = 0;
            titlePara.Range.Font.Size = 12;
        }

        private void AddTextParagraph(string text, ushort textSize, byte bold, byte cursive, byte underline, int spaceAfter = 12)
        {
            //Insert another paragraph.
            Word.Paragraph jokePara;
            object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            jokePara = wordDoc.Content.Paragraphs.Add(ref oRng);
            jokePara.Range.Text = text;
            jokePara.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            //jokePara.Format.LeftIndent = leftIntent; // отступ слева
            jokePara.Range.Font.Name = "Times New Roman";
            jokePara.Range.Font.Size = textSize;
            jokePara.Range.Font.Bold = bold;
            jokePara.Range.Font.Italic = cursive;

            if (underline == 0)
                jokePara.Range.Font.Underline = WdUnderline.wdUnderlineNone;
            else
                jokePara.Range.Font.Underline = WdUnderline.wdUnderlineThick;

            jokePara.Format.SpaceAfter = spaceAfter;
            jokePara.Range.InsertParagraphAfter();
            jokePara.Range.Font.Underline = WdUnderline.wdUnderlineNone;
        }

        public void AddJokeText(string jokeTitle,string jokeText, JokeTitleMode titleMode = (byte)0, ushort textSize = (ushort)12)
        {
            int spaceAfter = 0;

            jokeText += "\n";

            switch (titleMode)
            {
                case JokeTitleMode.WithTitles:
                    {
                        AddTextParagraph(jokeTitle, textSize, 0, 0, 1, spaceAfter);
                        AddTextParagraph(jokeText, textSize, 0, 0, 0, spaceAfter);
                        break;
                    }
                case JokeTitleMode.WithOutTitles:
                    {
                        AddTextParagraph(jokeText, textSize, 0, 0, 0, spaceAfter); 
                        break;
                    }
                case JokeTitleMode.OnlyTitles:
                    {
                        AddTextParagraph(jokeTitle, textSize, 0, 0, 0, 10); 
                        break;
                    }
            }

        }

        private string CorrectFileName(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsPunctuation(input[i]))
                {
                    result += "-";
                }
                else
                {
                    result += input[i];
                }
            }
            return result;
        }
    }//class
}//namespace