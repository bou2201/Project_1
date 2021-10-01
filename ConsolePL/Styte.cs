using System;
using System.Text;


namespace ConsoleAppPL
{
    public class Style
    {
        public void WriteAt(string text, int x_left, int y_top)
        {
            try
            {
                Console.SetCursorPosition(x_left, y_top);
                Console.Write(text);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ClearAt(int x_letf, int x_right, int y_top, int y_bott)
        {
            int row = y_bott - y_top;
            int column = x_right - x_letf;
            for(int i = 0; i < row; i++)
            {
                Console.SetCursorPosition(x_letf, y_top++);
                Console.Write(new string(' ', column)); 
            }
        }
        public bool StringExists(string key, string[] keys){
            bool result = false;;
            int count = keys.Length;
            for(int i = 0; i < count; i++){
                if(keys[i].ToUpper() == key.ToUpper()){
                    result = true;
                    break;
                }
            }
            return result;
        }

        public string ReadString(){
            var result = string.Empty;
            ConsoleKey key;
            do{
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if(key == ConsoleKey.Enter){
                    break;
                }
                else if (key == ConsoleKey.Backspace && result.Length > 0)
                {
                    Console.Write("\b \b");
                    result = result[0..^1];
                }
                else if((key == ConsoleKey.Escape) || (key == ConsoleKey.RightArrow) || (key == ConsoleKey.LeftArrow)){
                    return key.ToString();
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write(keyInfo.KeyChar);
                    result += keyInfo.KeyChar;
                }
            }while(key != ConsoleKey.Enter);
            Console.WriteLine();
            return result;
        }

        public void TextColor(string text, ConsoleColor color){
            ConsoleColor foreground = (color);
            Console.ForegroundColor = foreground;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}