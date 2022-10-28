using System.Text.Json;

class Program
{
    static void Main()
    {
        Kniga K = new();

        char vvod;
        do
        {
            Console.Clear();
            Console.WriteLine("1.Создание словаря\n2.Выбор словаря\n");
            Console.WriteLine("Esc - Выход\n");
            vvod = Console.ReadKey().KeyChar;
            switch (vvod)
            {
                case '1':
                    Console.Clear();
                    K.AddVocabulary();
                    Console.WriteLine("Press any key to continue.\n");
                    Console.ReadKey();
                    break;
                case '2':
                    Console.Clear();
                    if (K.GetCount() > 0)
                    {
                        K.PrintVocabularys();
                        string? str = "";
                        while (!Prover(str) || Convert.ToInt32(str) <= 0 || Convert.ToInt32(str) > K.GetCount())
                        {
                            Console.Write("Введите номер словаря: ");
                            str = Console.ReadLine();
                            if (!Prover(str) || Convert.ToInt32(str) <= 0 || Convert.ToInt32(str) > K.GetCount())
                                Console.WriteLine("Кривой ввод.");
                        }
                        if (Prover(str) && Convert.ToInt32(str) > 0 && Convert.ToInt32(str) < K.GetCount() + 1)
                        {
                            int id = Convert.ToInt32(str) - 1;
                            char vvod2;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("1.Добавить слово\n2.Добавить перевод слова\n3.Редактировать слово\n4.Редактировать перевод слова\n5.Найти перевод слова");
                                Console.WriteLine("6.Запись в файл\n7.Вывод всех слов.\n\nEsc - Предыдущее меню.\n");
                                vvod2 = Console.ReadKey().KeyChar;
                                switch (vvod2)
                                {
                                    case '1':
                                        Console.Clear();
                                        K.AddWord_(id);
                                        break;
                                    case '2':
                                        Console.Clear();
                                        K.AddTranslate(id);
                                        break;
                                    case '3':
                                        char vvod3;
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1.Изменить слово\n2.Удалить слово\nEsc - Предыдущее меню\n");
                                            vvod3 = Console.ReadKey().KeyChar;
                                            switch (vvod3)
                                            {
                                                case '1':
                                                    Console.Clear();
                                                    K.EditWord_(id);
                                                    break;
                                                case '2':
                                                    Console.Clear();
                                                    K.DelWord_(id);
                                                    break;
                                            }
                                        } while (vvod3 != 27);
                                        break;
                                    case '4':
                                        char vvod4;
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1.Изменить перевод\n2.Удалить перевод\nEsc - Предыдущее меню\n");
                                            vvod4 = Console.ReadKey().KeyChar;
                                            switch (vvod4)
                                            {
                                                case '1':
                                                    Console.Clear();
                                                    K.EditTranslate_(id);
                                                    break;
                                                case '2':
                                                    Console.Clear();
                                                    K.DelTranslate_(id);
                                                    break;
                                            }
                                        } while (vvod4 != 27);
                                        break;
                                    case '5':
                                        Console.Clear();
                                        Console.Write("Искомое слово: ");
                                        K.PrintWord_(K.SortByWord(id, Console.ReadLine()));
                                        break;
                                    case '6':
                                        Console.Clear();
                                        K.CreateFile(K.getVocabularys()[id]);
                                        break;
                                    case '7':
                                        Console.Clear();
                                        K.PrintAllWords(K.getVocabularys()[id]);
                                        break;
                                }
                                Console.WriteLine("Press any key to continue.\n");
                                Console.ReadKey();
                            } while (vvod2 != 27);
                        }
                        else
                        {
                            Console.WriteLine("Такого номера нет.");
                            Console.WriteLine("Press any key to continue.\n");
                            Console.ReadKey();
                        }
                    }
                    else
                        Console.WriteLine("Нет ни одного словаря.");
                    break;
            }
        } while (vvod != 27);

        bool Prover(string str)
        {
            if (str.Length > 0)
            {
                for (int i = 0; i < str.Length; i++)
                    if (!Char.IsDigit(str[i]))
                        return false;
                return true;
            }
            else return false;
        }
    }
}

class Kniga
{
    Vocabulary[] Vocabularys;
    public Kniga() { Vocabularys = new Vocabulary[0]; }
    public bool Prover(string str)
    {
        if (str.Length > 0)
        {
            for (int i = 0; i < str.Length; i++)
                if (!Char.IsDigit(str[i]))
                    return false;
            return true;
        }
        else return false;
    }
    public Vocabulary[] getVocabularys() { return Vocabularys; }
    public void PrintVocabularys()
    {
        for (int i = 0; i < Vocabularys.Length; i++)
            Console.WriteLine("[" + (i + 1) + "] " + Vocabularys[i].Name);
        Console.WriteLine();
    }
    public void PrintWord_(IOrderedEnumerable<Word_> pe)
    {
        if (pe.Count() > 0)
        {
            foreach (var p2 in pe)
            {
                Console.WriteLine("Слово: " + p2.Word);
                Console.Write("Перевод: ");
                foreach (var p3 in p2.Words_Translate)
                    Console.Write(p3 + " ");
            }
            Console.WriteLine();
        }
        else Console.WriteLine("Список пуст.");
    }
    public void AddVocabulary()
    {       
        Array.Resize(ref Vocabularys, Vocabularys.Length + 1);
        Vocabularys[Vocabularys.Length - 1] = new();
        string? str = "";
        while(str.Length < 1)
        {
            Console.Write("Имя словаря: ");
            str = Console.ReadLine();
            if (str.Length < 1) Console.WriteLine("Кривой ввод.");
        }        
        Vocabularys[Vocabularys.Length - 1].Name = str;
    }
    public void AddWord_(int i)
    {
        Array.Resize(ref Vocabularys[i].word, Vocabularys[i].word.Length + 1);
        Vocabularys[i].word[Vocabularys[i].word.Length - 1] = new();
        Console.Write("Добавляемое слово: ");
        Vocabularys[i].word[Vocabularys[i].word.Length - 1].Word = Console.ReadLine();
        Console.WriteLine("Перевод этого слова: ");
        Array.Resize(ref Vocabularys[i].word[Vocabularys[i].word.Length - 1].Words_Translate, Vocabularys[i].word[Vocabularys[i].word.Length - 1].Words_Translate.Length + 1);
        string? tmp = "";
        while(tmp.Length <= 0)
        {
            tmp = Console.ReadLine();
            if (tmp.Length <= 0) Console.WriteLine("Нужно ввести перевод слова "+ Vocabularys[i].word[Vocabularys[i].word.Length - 1].Word.ToUpper()+".");
        }
        Vocabularys[i].word[Vocabularys[i].word.Length - 1].Words_Translate[Vocabularys[i].word[Vocabularys[i].word.Length - 1].Words_Translate.Length - 1] = tmp;
    }
    public void AddTranslate(int i)
    {
        Console.WriteLine("К какому слову добавить перевод ?");
        string? str = "";
        str = Console.ReadLine();
        int u = FindID_Name(i,str);
 
        if (u >= 0)
        {
            Console.WriteLine("Перевод слова " + str + ": ");
            Array.Resize(ref Vocabularys[i].word[u].Words_Translate, Vocabularys[i].word[u].Words_Translate.Length + 1);
            Vocabularys[i].word[u].Words_Translate[Vocabularys[i].word[u].Words_Translate.Length - 1] = Console.ReadLine();
        }
        else
            Console.WriteLine("Слово не найдено.");
    }
    public int FindID_Name(int i,string? str)
    {
        int u = 0;
        bool f = false;
        for (; u < Vocabularys[i].word.Length; u++)
            if (Vocabularys[i].word[u].Word == str)
            {
                f = true;
                break;
            }
        if (f) return u;
        else return -1;
    }
    public void EditWord_(int i)
    {
        Console.Write("Слово для замены: ");
        string? str = Console.ReadLine();
        int u = FindID_Name(i, str);
        if (u >= 0)
        {
            Console.Write("Новое слово: ");
            Vocabularys[i].word[u].Word = Console.ReadLine();
        }
        else
            Console.WriteLine("Не найдено.");
    }
    public void EditTranslate_(int i)
    {
        Console.Write("Оригинальное слово: ");
        string? str = Console.ReadLine();
        int u = FindID_Name(i, str);
        if (u >= 0)
        {
            string? str2 = "-1";
            for (int k = 0; k < Vocabularys[i].word[u].Words_Translate.Length; k++)
                Console.WriteLine("[" + (k + 1) + "] " + Vocabularys[i].word[u].Words_Translate[k]);

            while (Convert.ToInt32(str2) < 1 || Convert.ToInt32(str2) > Vocabularys[i].word[u].Words_Translate.Length)
            {
                Console.Write("Введите id слова для изменения: ");
                str2 = Console.ReadLine();
                if (Convert.ToInt32(str2) < 1 || Convert.ToInt32(str2) > Vocabularys[i].word[u].Words_Translate.Length)
                    Console.WriteLine("Кривой ввод.");
            }
            int l = Convert.ToInt32(str2) - 1;

            Vocabularys[i].word[u].Words_Translate[l] = Console.ReadLine();
        }
        else
            Console.WriteLine("Не найдено.");
    }
    public void DelWord_(int i)
    {
        Console.Write("Слово для удаления: ");
        string? str = Console.ReadLine();
        int u = FindID_Name(i,str);
        if (u>=0)
        {
            for (int k = u; k < Vocabularys[i].word.Length - 1; k++)
            {
                Vocabularys[i].word[k] = Vocabularys[i].word[k + 1];
            }
            Array.Resize(ref Vocabularys[i].word, Vocabularys[i].word.Length - 1);
        }
        else
            Console.WriteLine("Не найдено.");
    }
    public void DelTranslate_(int i)
    {
        Console.Write("Оригинальное слово: ");
        string? str = Console.ReadLine();
        int u = FindID_Name(i, str);
        if (u >= 0)
        {
            if (Vocabularys[i].word[u].Words_Translate.Length > 1)
            {
                string? str2 = "-1";
                for (int k = 0; k < Vocabularys[i].word[u].Words_Translate.Length; k++)
                    Console.WriteLine("[" + (k + 1) + "] " + Vocabularys[i].word[u].Words_Translate[k]);

                while (Convert.ToInt32(str2) < 1 || Convert.ToInt32(str2) > Vocabularys[i].word[u].Words_Translate.Length)
                {
                    Console.Write("Введите id слова для удаления: ");
                    str2 = Console.ReadLine();
                    if (Convert.ToInt32(str2) < 1 || Convert.ToInt32(str2) > Vocabularys[i].word[u].Words_Translate.Length)
                        Console.WriteLine("Кривой ввод.");
                }
                int l = Convert.ToInt32(str2) - 1;
                for (int s = l; s < Vocabularys[i].word[u].Words_Translate.Length - 1; s++)
                    Vocabularys[i].word[u].Words_Translate[l] = Vocabularys[i].word[u].Words_Translate[l + 1];

                Array.Resize(ref Vocabularys[i].word[u].Words_Translate, Vocabularys[i].word[u].Words_Translate.Length - 1);
            }
            else
                Console.WriteLine("Нельзя удалить единственный перевод.");
        }
        else
            Console.WriteLine("Не найдено.");
    }
    public int GetCount() { return Vocabularys.Length; }
    public IOrderedEnumerable<Word_> SortByWord(int id,string? str)
    {
        var sort = from p in Vocabularys[id].word
                            where p.Word.ToUpper() == str.ToUpper()
                            orderby p.Word
                            select p;
        return sort;
    }
    public async void CreateFile(Vocabulary p)
    {        
        try
        {
            using (FileStream f = new(p.Name + ".json", FileMode.Create))
            {              
                var op = new JsonSerializerOptions { WriteIndented = true };
                foreach (var v in p.word)
                {
                    await JsonSerializer.SerializeAsync(f, v, op);
                    await JsonSerializer.SerializeAsync(f, v.Words_Translate, op);
                }
            }
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void PrintAllWords(Vocabulary p)
    {
        foreach(var word in p.word)
        {
            Console.Write("Слово: " + word.Word + "\nПеревод: ");
            foreach(var v in word.Words_Translate)
                Console.Write(v + " ");
            Console.WriteLine('\n');
        }
        Console.WriteLine();
    }
}
class Vocabulary
{
    public Word_[] word;
    public string?Name { get; set; }
    public Vocabulary() { word = new Word_[0]; }
    public Vocabulary(string? name,Word_[] Word) { Name = name; word = Word; word = new Word_[0]; }
}
class Word_
{
    public string?[] Words_Translate;
    public string? Word { get; set; }
    public Word_() { Words_Translate = new string?[0]; }
    public Word_(string? word, string? words_translate) 
    {
        Word = word;
        Words_Translate = words_translate.Split(new char[] { });
    }
}