using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace merginto
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            


            const string appName = "merginto";
            const string appTitle = "[ mergeinto Console ] ";
            
            
            Console.WindowWidth = Console.LargestWindowWidth * 9 / 10;
            Console.WindowHeight = Console.LargestWindowHeight * 4 / 10;
            Console.Title = appName;
            Console.WriteLine(appTitle);

            #region Args Parsing
            
            string 
                pathComicsDir = null,
                pathOutputDir = null;
            bool hideTitle = false;
            bool dontAskAgain = false;
            
            int argsCount = args.Length;
            if (argsCount >= 1) // Parse Args
            {
                for (int argIndex = 0; argIndex < argsCount; argIndex += 1)
                {
                    switch (args[argIndex].ToUpper())
                    {
                        case "/INPUT":
                        case "-INPUT":
                        case "/I":
                        case "-I":
                        case "/IN":
                        case "-IN":
                            pathComicsDir = args[argIndex + 1];
                            argIndex += 1;
                            break;

                        case "/OUTPUT":
                        case "-OUTPUT":
                        case "/OUT":
                        case "-OUT":
                        case "/O":
                        case "-O":
                            pathOutputDir = args[argIndex + 1];
                            argIndex += 1;
                            break;

                        case "/HELP":
                        case "-HELP":
                        case "/?":
                            HelpPrint();
                            return 0;

                        case "/HIDETITLE":
                        case "-HIDETITLE":
                        case "/H":
                        case "-H":
                            hideTitle = true;
                            break;

                        case "/Y":
                        case "-Y":
                        case "/YES":
                        case "-YES":
                            dontAskAgain = true;
                            break;

                        default:
                            WriteLine("Cannot Parse Arguments!", ConsoleColor.Red);
                            HelpPrint();
                            return -1;

                    }
                }
            }
            else // Receive Input
            {
                
                //WriteLine(@"          _____                    _____                    _____                    _____                    _____                    _____                _____                   _______", ConsoleColor.Cyan);         
                //WriteLine(@"         /\    \                  /\    \                  /\    \                  /\    \                  /\    \                  /\    \              /\    \                 /::\    \", ConsoleColor.Cyan);        
                //WriteLine(@"        /::\____\                /::\    \                /::\    \                /::\    \                /::\    \                /::\____\            /::\    \               /::::\    \", ConsoleColor.Cyan);       
                //WriteLine(@"       /::::|   |               /::::\    \              /::::\    \              /::::\    \               \:::\    \              /::::|   |            \:::\    \             /::::::\    \", ConsoleColor.Cyan);      
                //WriteLine(@"      /:::::|   |              /::::::\    \            /::::::\    \            /::::::\    \               \:::\    \            /:::::|   |             \:::\    \           /::::::::\    \", ConsoleColor.Cyan);     
                //WriteLine(@"     /::::::|   |             /:::/\:::\    \          /:::/\:::\    \          /:::/\:::\    \               \:::\    \          /::::::|   |              \:::\    \         /:::/~~\:::\    \", ConsoleColor.Cyan);    
                //WriteLine(@"    /:::/|::|   |            /:::/__\:::\    \        /:::/__\:::\    \        /:::/  \:::\    \               \:::\    \        /:::/|::|   |               \:::\    \       /:::/    \:::\    \", ConsoleColor.Cyan);   
                //WriteLine(@"   /:::/ |::|   |           /::::\   \:::\    \      /::::\   \:::\    \      /:::/    \:::\    \              /::::\    \      /:::/ |::|   |               /::::\    \     /:::/    / \:::\    \", ConsoleColor.Cyan);  
                //WriteLine(@"  /:::/  |::|___|______    /::::::\   \:::\    \    /::::::\   \:::\    \    /:::/    / \:::\    \    ____    /::::::\    \    /:::/  |::|   | _____        /::::::\    \   /:::/____/   \:::\____\", ConsoleColor.Cyan); 
                //WriteLine(@" /:::/   |::::::::\    \  /:::/\:::\   \:::\    \  /:::/\:::\   \:::\____\  /:::/    /   \:::\ ___\  /\   \  /:::/\:::\    \  /:::/   |::|   |/\    \      /:::/\:::\    \ |:::|    |     |:::|    |", ConsoleColor.Cyan);
                //WriteLine(@"/:::/    |:::::::::\____\/:::/__\:::\   \:::\____\/:::/  \:::\   \:::|    |/:::/____/  ___\:::|    |/::\   \/:::/  \:::\____\/:: /    |::|   /::\____\    /:::/  \:::\____\|:::|____|     |:::|    |", ConsoleColor.Cyan);
                //WriteLine(@"\::/    / ~~~~~/:::/    /\:::\   \:::\   \::/    /\::/   |::::\  /:::|____|\:::\    \ /\  /:::|____|\:::\  /:::/    \::/    /\::/    /|::|  /:::/    /   /:::/    \::/    / \:::\    \   /:::/    /", ConsoleColor.Cyan); 
                //WriteLine(@" \/____/      /:::/    /  \:::\   \:::\   \/____/  \/____|:::::\/:::/    /  \:::\    /::\ \::/    /  \:::\/:::/    / \/____/  \/____/ |::| /:::/    /   /:::/    / \/____/   \:::\    \ /:::/    /", ConsoleColor.Cyan);  
                //WriteLine(@"             /:::/    /    \:::\   \:::\    \            |:::::::::/    /    \:::\   \:::\ \/____/    \::::::/    /                   |::|/:::/    /   /:::/    /             \:::\    /:::/    /", ConsoleColor.Cyan);   
                //WriteLine(@"            /:::/    /      \:::\   \:::\____\           |::|\::::/    /      \:::\   \:::\____\       \::::/____/                    |::::::/    /   /:::/    /               \:::\__/:::/    /", ConsoleColor.Cyan);    
                //WriteLine(@"           /:::/    /        \:::\   \::/    /           |::| \::/____/        \:::\  /:::/    /        \:::\    \                    |:::::/    /    \::/    /                 \::::::::/    /", ConsoleColor.Cyan);     
                //WriteLine(@"          /:::/    /          \:::\   \/____/            |::|  ~|               \:::\/:::/    /          \:::\    \                   |::::/    /      \/____/                   \::::::/    /", ConsoleColor.Cyan);      
                //WriteLine(@"         /:::/    /            \:::\    \                |::|   |                \::::::/    /            \:::\    \                  /:::/    /                                  \::::/    /", ConsoleColor.Cyan);       
                //WriteLine(@"        /:::/    /              \:::\____\               \::|   |                 \::::/    /              \:::\____\                /:::/    /                                    \::/____/", ConsoleColor.Cyan);        
                //WriteLine(@"        \::/    /                \::/    /                \:|   |                  \::/____/                \::/    /                \::/    /                                      ~~", ConsoleColor.Cyan);              
                //WriteLine(@"         \/____/                  \/____/                  \|___|                                            \/____/                  \/____/", ConsoleColor.Cyan);

                string[] autoFolders = Directory.GetDirectories(System.Environment.CurrentDirectory);
                bool _inputDefined = false, _outputDefined = false;
                foreach (string folder in autoFolders)
                {
                    switch (Path.GetFileName(folder).ToUpper())
                    {
                        case "INPUT":
                            case "IN":
                            _inputDefined = true;
                            pathComicsDir = folder;
                            break;
                        
                        case "OUTPUT":
                            case "OUT":
                            _outputDefined = true;
                            pathOutputDir = folder;
                            break;
                    }
                }
                
                //WriteLine("\n언제든 종료하고 싶으시면 Ctrl+C를 입력해 주세요.",ConsoleColor.Magenta);
                //WriteLine("폴더 혹은 파일의 이름이 너무 길거나 (70문자 이상) 알 수 없는 문자가 포함된 경우 오류가 발생합니다!",ConsoleColor.Magenta);
                
                Write("\n\n");
                
                if (_inputDefined)
                {
                    WriteLine("작품 입력 폴더가 자동 감지되었습니다!",ConsoleColor.DarkGray);
                    WriteLine("Input Folder Auto-Detected!",ConsoleColor.DarkGray);
                    WriteLine($"IN: {pathComicsDir}",ConsoleColor.DarkGray);
                }
                else
                {
                    WriteLine("입력 폴더(\"Input\")가 감지되지 않았습니다. 직접 설정해 주세요.");
                    WriteLine("The input folder could not be found. Please Set Manually.");
                    Write("\n");
                    WriteLine("작품 폴더들이 들어있는 폴더의 경로를 입력하거나 이 창으로 드래그하세요. (입력 후 Enter)",ConsoleColor.Yellow);
                    Write("Please Enter full path or Drag the folder containing the folders, including comic image files.\n>",ConsoleColor.Yellow);
                    pathComicsDir = Console.ReadLine();
                }
                
                Write("\n\n");

                if (_outputDefined)
                {
                    WriteLine("PDF 출력 폴더가 자동 감지되었습니다!",ConsoleColor.DarkGray);
                    WriteLine("Output Folder Auto-Detected!",ConsoleColor.DarkGray);
                    WriteLine($"OUT: {pathOutputDir}",ConsoleColor.DarkGray);
                }
                else
                {
                    WriteLine("출력 폴더(\"Output\")가 감지되지 않았습니다. 직접 설정해 주세요.");
                    WriteLine("The output folder could not be found. Please Set Manually.");
                    Write("\n");
                    WriteLine("생성된 PDF 파일들이 들어갈 폴더의 경로를 입력하거나 이 창으로 드래그하세요. (입력 후 Enter)",ConsoleColor.Yellow);
                    Write("Please Enter full path or Drag the folder in which the PDF files created will be placed.\nEx) E:\\WorkingSpace\\OutPut\n>",ConsoleColor.Yellow);
                    pathOutputDir = Console.ReadLine();
                }
                Write("\n");
                WriteLine("작업 중 이 창으로 로그를 출력할 때, 작품의 이름(폴더명)을 숨길까요? ( Y = 숨긴다, N = 숨기지 않는다 ) (입력 후 Enter)",ConsoleColor.Yellow);
                Write("When printing the log in progress, should this program hide the title of the cartoon? ( y = hide title, n = show title(default) )\n>",ConsoleColor.Yellow);
                ConsoleKeyInfo readKey = Console.ReadKey();
                hideTitle = readKey.Key == ConsoleKey.Y;
            }
            
            #endregion

            #region Argument Checking
            
            if (pathComicsDir == null || pathOutputDir == null)
            {
                Write("Params Error!\nInput and output folders must be defined!", ConsoleColor.Red);
                return -1;
            }
            
            bool
                _pathComicsDirExists = Directory.Exists(pathComicsDir),
                _pathOutputDirExists = Directory.Exists(pathOutputDir);

            if (!(_pathComicsDirExists && _pathOutputDirExists)) // Input folder or Output Folder not exists
            {
                WriteLine("ERROR: 작품 폴더들이 들어있는 폴더 혹은 출력 폴더가 존재하지 않습니다!", ConsoleColor.Red);
                WriteLine("다른 경로를 입력하거나 폴더를 생성한 후 다시 시도해 주세요.", ConsoleColor.Red);
                WriteLine("ERROR: Path not exists!", ConsoleColor.Red);
                WriteLine($"Input Folder {pathComicsDir} ... {_pathComicsDirExists}",
                    _pathComicsDirExists ? ConsoleColor.Green : ConsoleColor.Red);
                WriteLine($"Output Folder {pathOutputDir} ... {_pathOutputDirExists}",
                    _pathOutputDirExists ? ConsoleColor.Green : ConsoleColor.Red);
                Environment.Exit(0);
                return 0;
            }
            
            
            #endregion

            



            string[] failureList = null;
            int failureCount = 0;
            
            WriteLine("\n작품 가져오는 중...", ConsoleColor.Gray);
            WriteLine("Finding Comics...\n", ConsoleColor.Gray);
            
            string[] comicFolders = Directory.GetDirectories(pathComicsDir); // Comics
            Array.Sort(comicFolders, new FileNameComparer());

            int totalComicsCount = comicFolders.Length; // Comics Count
            int totalImagesCount = 0;
            for (int i = 0; i < totalComicsCount; i++)
            {
                string _comicFolder = comicFolders[i];
                string _comicName = hideTitle ? $"Comic {i + 1:D4}" : $"{Path.GetFileName(_comicFolder)}";

                int _comicImageCount = Directory.GetFiles(_comicFolder).Length;
                totalImagesCount += _comicImageCount; // Images Count
                Write($"[{i + 1:D4}/{totalComicsCount:D4}] ", ConsoleColor.DarkCyan);
                Write($"{_comicName}  ");
                Write("...", ConsoleColor.DarkGray);
                WriteLine($"{_comicImageCount,4} Images");
            }

            if (!dontAskAgain)
            {
                Write("\n");
                WriteLine($"Output Path: {pathOutputDir}\n\tex) {pathOutputDir}\\WebToon1.pdf",ConsoleColor.Cyan);
                Write("\n");
                WriteLine($"{totalComicsCount} 개의 작품들을 (이미지 {totalImagesCount}개) {totalComicsCount}개의 PDF 파일로 변환하여 위의 경로에 생성할까요?", ConsoleColor.Yellow);
                Write("\n");
                WriteLine("   - y가 입력될 시 위의 모든 작업을 시작합니다! 창을 닫지 말고 켠 채로 유지해주세요!", ConsoleColor.Gray);
                WriteLine("   - 작업을 중지하고 싶으시면 창을 닫거나 Ctrl+C를 입력해 주세요!", ConsoleColor.Gray);
                WriteLine("   - 이후 폴더 이름을 변경하실 경우 해당 작품이 스킵될 수 있습니다!", ConsoleColor.Gray);
                WriteLine("   - 만약 위의 작품들 중 이름이 너무 길거나 (70+) 알 수 없는 문자가 포함되었을 경우 수정 후 다시 실행해 주세요!", ConsoleColor.Gray);
                WriteLine("   - PNG 파일로 이루어진 작품의 경우 시간이 오래 걸릴 수 있습니다!", ConsoleColor.Gray);
                WriteLine(ConsoleColor.Gray, "   - 작업 진척도를 올바르게 확인하시려면 현재 콘솔 창을 충분히 넓게 늘려주세요!");
                Write("\n");
                WriteLine($"Convert {totalComicsCount} cartoon image folders ({totalImagesCount} images) in a into PDF files and create them in this folder?",ConsoleColor.Yellow); 
                
                Write($"(Y/N) : ",ConsoleColor.Yellow);

                ConsoleKey key = Console.ReadKey().Key;
                if (key != ConsoleKey.Y)
                {
                    WriteLine("\nTerminated!", ConsoleColor.Green);
                    WriteLine("사용자에 의해 종료됨!", ConsoleColor.Green);
                    WriteLine("\nPress any ket to exit", ConsoleColor.Green);
                    Console.ReadKey();
                    return 0;
                }
            }
            
            
            Write("\n작업을 시작합니다!\n",ConsoleColor.Yellow);
            Console.Clear();
            Console.CursorVisible = false;

            int __v = 0;
            int totalImagesIndex = 1; // Total Image Index
            for (int comicProcessingIndex = 1; comicProcessingIndex <= totalComicsCount; comicProcessingIndex++)
            {
                string
                    comicPath =
                        comicFolders
                            [comicProcessingIndex - 1], // Current Processing Comic Folder's Full Path "C:\Comics\WebToon1"
                    _comicRealName =
                        Path.GetFileName(comicPath), // Current Processing Comic Folder's Real Folder Name "WebToon1"
                    _comicNumericalName =
                        $"COMIC_{comicProcessingIndex:D4}", // Current Processing Comic Folder's Numerical Name "COMIC_0001"
                    comicName =
                        hideTitle
                            ? _comicNumericalName
                            : _comicRealName, // Current Processing Comic's Name for Display "WebToon1" or "COMIC_0001"

                    pdfFullPath = Path.Combine(pathOutputDir, _comicRealName + ".pdf");

                if (!(Directory.Exists(comicPath) && Directory.Exists(pathOutputDir)))
                {
                    WriteLine($"Folder {comicPath} or {pathOutputDir} not exists!", ConsoleColor.Red);
                    WriteLine($"작품들 폴더 {comicPath} 혹은 출력 폴더 {pathOutputDir}가 존재하지 않습니다!", ConsoleColor.Red);
                    WriteLine("Skipping...", ConsoleColor.Yellow);
                    WriteLine("해당 작품 건너뛰는 중 ...", ConsoleColor.Yellow);
                    failureList[failureCount++] = comicPath;
                    continue;
                }
                float comicProcess = 100f * comicProcessingIndex / totalComicsCount;

                
                
                string[] images = Directory.GetFiles(comicPath);
                Array.Sort(images, new FileNameComparer());
                int imagesCount = images.Length;
                PdfDocument pdf = new PdfDocument();
                
                WriteLine(String.Format(
                    "Processing Comic [{0,-4}/{1,-4}] : {2} : {3} Images",
                    comicProcessingIndex.ToString("D4"), totalComicsCount.ToString("D4"),
                    comicName,
                    imagesCount
                ), ConsoleColor.DarkGray);

                string lastStatus = "";
                for (int pageIndex = 0; pageIndex < imagesCount; pageIndex += 1, totalImagesIndex++)
                {
                    int page = pageIndex + 1;
                    string imagePath = images[pageIndex];
                    string imageName = Path.GetFileName(imagePath);

                    PdfPage pdfPage = pdf.AddPage();
                    XImage ximage = XImage.FromFile(imagePath);
                    XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                    double
                        height = ximage.Size.Height,
                        width = ximage.Size.Width;


                    pdfPage.Height = height;
                    pdfPage.Width = width;

                    float
                        currentComicPageProcess = 100f * page / imagesCount ,
                        totalImageProcess = 100f * totalImagesIndex / totalImagesCount;

                    char ving = vingle();
                    string status = String.Format(

                        " {13} >>> {9} ({10}x{11}) => Page {12} ... COMIC {3}% [{4}/{5}] : PAGE {6}% [{7}/{8}] / TOTAL {0}% [{1}/{2}]        ",

                        totalImageProcess, totalImagesIndex, totalImagesCount,
                        comicProcess, comicProcessingIndex, totalComicsCount,
                        currentComicPageProcess, page, imagesCount,
                        imageName, width, height, page,
                        ving
                    );
                    lastStatus = status;

                    Console.Write("\r" + status);
                    Console.Title = $"{ving} Merginto : Processing - [{totalImagesIndex}/{totalImagesCount}] : {totalImageProcess:F2}%";

                    

                    gfx.DrawImage(ximage, 0, 0);

                }
                

                pdf.Save(pdfFullPath);
                WriteLine("\r" + lastStatus + " ... Fin!         \t", ConsoleColor.Green);


                
                char vingle()
                {
                    char[] _arr = new[] { '↗', '→', '↘', '↓', '↙', '←', '↖', '↑' };

                    __v++;
                    if (__v == 8) __v = 0;
                    return _arr[__v];
                }
            }
            
            WriteLine(string.Format(
                "처리한 작업: 작품 {0}개, 이미지 {1}개\n" +
                "Task Finished: {0} Comics, {1} Images\n",
                totalComicsCount, totalImagesIndex
                ),ConsoleColor.Yellow);
            
            WriteLine("모든 작업이 완료되었습니다!",ConsoleColor.Cyan);
            WriteLine("Totally Finished!!!",ConsoleColor.Cyan);

            if (failureList != null)
            {
                foreach (var fail in failureList)
                {
                    WriteLine(fail);
                }
            }
            else
            {
                WriteLine("실패한 작업이 없습니다.");
                WriteLine("No fails!");
            }

            Console.ReadKey();
            return 0;
        }

        private static void WriteLine(object o, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(o);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Write(object o, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(o);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void HelpPrint()
        {
            Write("\n\nExample: ", ConsoleColor.Yellow);
            WriteLine("D:\\ITP.exe /i D:\\Comics /o D:\\CreatedPdfs /hidetitle");

            Write("\nAvailable Switches\n", ConsoleColor.Yellow);


            WriteLine("\n\t/i <folderpath>:", ConsoleColor.Cyan);
            WriteLine("\t\tSpecifies full path of the folder containing the folders, including animated image files.");
            WriteLine("\t\tAliases: /i /in /input -i -in -input");
            WriteLine("\t\tUsage: /i <full path of comics folder>");
            WriteLine("\t\tExample: /i C:\\Comics");

            WriteLine("\n\t/o <folderpath>:", ConsoleColor.Cyan);
            WriteLine("\t\tSpecifies the folder in which the PDF files created will be placed.");
            WriteLine("\t\tAliases: /i /in /input -i -in -input");
            WriteLine("\t\tUsage: /i <full path of output folder>");
            WriteLine("\t\tExample: /i C:\\PDFs");

            WriteLine("\n\t/? :", ConsoleColor.Cyan);
            WriteLine("\t\tShows this message");
            WriteLine("\t\tAliases: /? /help -help");

            WriteLine("\n\t/h :", ConsoleColor.Cyan);
            WriteLine("\t\tReplace the name of comics with Numeric Name");
            WriteLine("\t\tAliases: /h /hidetitle -h -hidetitle");

            WriteLine("\n\t/y :", ConsoleColor.Cyan);
            WriteLine("\t\tDo not ask any question and try process directly.");
            WriteLine("\t\tAliases: /y /yes -Y -YES");
        }


    }

    public class FileNameComparer : IComparer<string>
    {
        public FileNameComparer()
        {
            
        }

        public int Compare(object x, object y)
        {
            if ((x is string) && (y is string))
            {
                return StringLogicalComparer.Compare((string)x, (string)y);
            }

            return -1;
        }

        public int Compare(string x, string y)
        {
            return StringLogicalComparer.Compare(x, y);
        }

        private class StringLogicalComparer
        {
            public static int Compare(string s1, string s2)
            {
                if ((s1 == null) && (s2 == null)) return 0;
                else if (s1 == null) return -1;
                else if (s2 == null) return 1;

                if ((s1.Equals(string.Empty) && (s2.Equals(string.Empty)))) return 0;
                else if (s1.Equals(string.Empty)) return -1;
                else if (s2.Equals(string.Empty)) return -1;
                
                bool sp1 = Char.IsLetterOrDigit(s1, 0);
                bool sp2 = Char.IsLetterOrDigit(s2, 0);
                if (sp1 && !sp2) return 1;
                if (!sp1 && sp2) return -1;

                int i1 = 0, i2 = 0; 
                int r = 0; 
                while (true)
                {
                    bool c1 = Char.IsDigit(s1, i1);
                    bool c2 = Char.IsDigit(s2, i2);
                    if (!c1 && !c2)
                    {
                        bool letter1 = Char.IsLetter(s1, i1);
                        bool letter2 = Char.IsLetter(s2, i2);
                        if ((letter1 && letter2) || (!letter1 && !letter2))
                        {
                            if (letter1 && letter2)
                            {
                                r = Char.ToLower(s1[i1]).CompareTo(Char.ToLower(s2[i2]));
                            }
                            else
                            {
                                r = s1[i1].CompareTo(s2[i2]);
                            }

                            if (r != 0) return r;
                        }
                        else if (!letter1 && letter2) return -1;
                        else if (letter1 && !letter2) return 1;
                    }
                    else if (c1 && c2)
                    {
                        r = CompareNum(s1, ref i1, s2, ref i2);
                        if (r != 0) return r;
                    }
                    else if (c1)
                    {
                        return -1;
                    }
                    else if (c2)
                    {
                        return 1;
                    }

                    i1++;
                    i2++;
                    if ((i1 >= s1.Length) && (i2 >= s2.Length))
                    {
                        return 0;
                    }
                    else if (i1 >= s1.Length)
                    {
                        return -1;
                    }
                    else if (i2 >= s2.Length)
                    {
                        return -1;
                    }
                }
            }

            private static int CompareNum(string s1, ref int i1, string s2, ref int i2)
            {
                int nzStart1 = i1, nzStart2 = i2; // nz = non zero
                int end1 = i1, end2 = i2;

                ScanNumEnd(s1, i1, ref end1, ref nzStart1);
                ScanNumEnd(s2, i2, ref end2, ref nzStart2);
                int start1 = i1;
                i1 = end1 - 1;
                int start2 = i2;
                i2 = end2 - 1;

                int nzLength1 = end1 - nzStart1;
                int nzLength2 = end2 - nzStart2;

                if (nzLength1 < nzLength2) return -1;
                else if (nzLength1 > nzLength2) return 1;

                for (int j1 = nzStart1, j2 = nzStart2; j1 <= i1; j1++, j2++)
                {
                    int r = s1[j1].CompareTo(s2[j2]);
                    if (r != 0) return r;
                }

                // the nz parts are equal
                int length1 = end1 - start1;
                int length2 = end2 - start2;
                if (length1 == length2) return 0;
                if (length1 > length2) return -1;
                return 1;
            }

            private static void ScanNumEnd(string s, int start, ref int end, ref int nzStart)
            {
                nzStart = start;
                end = start;
                bool countZeros = true;
                while (Char.IsDigit(s, end))
                {
                    if (countZeros && s[end].Equals('0'))
                    {
                        nzStart++;
                    }
                    else countZeros = false;

                    end++;
                    if (end >= s.Length) break;
                }
            }
        }
    }
}
    