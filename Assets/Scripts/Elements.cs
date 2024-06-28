using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour
{
    public static Element[] elements;
    public class Element
    {
        private int index;
        private string en_title;
        private string ru_title;
        private bool final;
        public Element(int index, string en_title, string ru_title, bool final)
        {
            this.index = index;
            this.en_title = en_title;
            this.ru_title = ru_title;
            this.final = final;
        }
        public string getTitle() 
        {
            if(Language.Instance.CurrentLan == "ru")
            {
                return ru_title;
            } else
            {
                return en_title;
            }
        }
        public bool getIsFinal() { return final; }
    }

    public void OnAwake()
    { 
        elements = new Element[]
        {
            new Element(0, "water", "вода", false), // 0вода
            new Element(1, "fire", "огонь", false), // 1огонь
            new Element(2, "stone", "камень", false), // 2камень
            new Element(3, "air", "воздух", false), // 3воздух
            new Element(4, "steam", "пар", false), // 0вода + 1огонь = пар
            new Element(5, "cley", "глина", false), // 0вода + 2камень = глина
            new Element(6, "ice", "лед", false), // 0вода + 3воздух = лед
            new Element(7, "lava", "лава", false), // 1огонь + 2камень = лава
            new Element(8, "light", "свет", false), // 1огонь + 3воздух = свет
            new Element(9, "sand", "песок", false), // 2камень + 3воздух = песок
            new Element(10, "obsidian", "обсидиан", true), // 0вода + 7лава = обсидиан
            new Element(11, "dirt", "грязь", false), // 0вода + 9песок = грязь
            new Element(12, "ceramics", "керамика", false), // 1огонь + 5глина = керамика (черепица)
            new Element(13, "ore", "руда", false), // 7лава + 9песок = руда
            new Element(14, "comet", "комета", false), // 2камень + 6лед = комета
            new Element(15, "vulcan", "вулкан", false), // 2камень + 7лава = вулкан
            new Element(16, "explosion", "взрыв", true), // 0вода + 15вулкан = взрыв
            new Element(17, "cloud", "облако", false), // 3воздух + 4пар = облако
            new Element(18, "glass", "стекло", false), // 1огонь + 9песок = стекло
            new Element(19, "cement", "цемент", false), // 5глина + 9песок = цемент
            new Element(20, "mountain", "гора", false), // 6лед + 15вулкан = гора*
            new Element(21, "wind", "ветер", false), // 3воздух + 20гора = ветер
            new Element(22, "rain", "дождь", false), // 0вода + 17облако = дождь
            new Element(23, "thunderstorm", "гроза", false), // 21ветер + 22дождь = гроза
            new Element(24, "rainbow", "радуга", true), // 8свет + 22дождь = радуга
            new Element(25, "asteroid", "астероид", false), // 13руда + 14комета = астероид
            new Element(26, "meteorite", "метеорит", false), // 3воздух/27земля + 25астероид/14комета = метеорит
            new Element(27, "earth", "земля", false), // 5глина + 11грязь = земля
            new Element(28, "iron", "железо", false), // 13руда + 27земля = железо
            new Element(29, "lightning", "молния", false), // 23гроза + 28железо = молния
            new Element(30, "desert", "пустыня", false), // 8свет + 9песок = пустыня
            new Element(31, "savannah", "саванна", false), // 0вода/22дождь + 30пустыня = саванна
            new Element(32, "grassland", "луга", false), // 0вода/22дождь + 27земля = луга
            new Element(33, "swamp", "болото", false), // 22дождь + 32луга = болото
            new Element(34, "snow", "снег", false), // 6лед + 22дождь = снег
            new Element(35, "crater", "кратер", false), // 26метеорит + 32луга/31саванна/30пустыня = кратер
            new Element(36, "lake", "озеро", false), // 0вода/22дождь + 35кратер = озеро
            new Element(37, "river", "река", false), // 0вода/22дождь + 20гора = река
            new Element(38, "ocean", "океан", false), // 36озеро + 37река = океан
            new Element(39, "salt", "соль", true), // 1огонь/30пустыня + 38океан = соль
            new Element(40, "bacteria", "бактерия", false), // 8свет + 38океан = жизнь(бактерии)
            new Element(41, "moss", "мох", false), // 27земля/32луга/31саванна + 40жизнь = растения(мох)
            new Element(42, "clam", "моллюск", false), // 0вода/38океан + 40жизнь = животные(моллюск)
            new Element(43, "mushroom", "гриб", false), // 33болото + 40жизнь = гриб(поганка)
            new Element(44, "plains", "равнина", false), // 8свет/31саванна + 32луга = равнина
            new Element(45, "cactus", "кактус", true), // 41растение + 30пустыня = кактус
            new Element(46, "tree", "дерево", false), // 41растение + 31саванна = дерево
            new Element(47, "flower", "цветок", true), // 41растение + 32луга = цветок
            new Element(48, "berry", "ягоды", true), // 41растение + 33болото = ягоды
            new Element(49, "seaweed", "водоросли", true), // 41растение + 36озеро = водоросли
            new Element(50, "сoral", "оралл", true), // 41растение + 38океан = коралл
            new Element(51, "forest", "лес", false), // 46дерево + 32луга = лес
            new Element(52, "coal", "уголь", false), // 46дерево + 1огонь = уголь
            new Element(53, "steel", "сталь", false), // 52уголь + 28железо = сталь
            new Element(54, "electricity", "электричество", false), // 53сталь + 29молния = электричество
            new Element(55, "fish", "рыбы", false), // 42животное + 36озеро/38океан = рыбы
            new Element(56, "frog", "лягушка", false), // 42животное/55рыбы + 33болото = лягушка
            new Element(57, "lizard", "ящерица", false), // 42животное/56земноводные + 30пустыня = ящерица
            new Element(58, "dinosaur", "динозавры", false), // 42животные/57пресмыкающиеся + 31саванна/32луга = динозавры
            new Element(59, "jungle", "джунгли", false), // 51лес + 0вода/22дождь = джунгли
            new Element(60, "mouse", "мышка", false), // 58динозавры + 26метеорит/15вулкан = мышка
            new Element(61, "concrete", "бетон", false), // 19цемент + 0вода = бетон
            new Element(62, "brick", "кирпич", false), // 19цемент + 12керамика = кирпич
            new Element(63, "foundation", "фундамент", false), // 61бетон + 53сталь = фундамент (дом 1/4)
            new Element(64, "wall", "стена", false), // 63фундамент + 62кирпич = стена (дом 2/4)
            new Element(65, "window", "окно", false), // 64стена + 18стекло = окно (дом 3/4)
            new Element(66, "house", "дом", false), // 65окно + 12керамика = дом
            new Element(67, "fenech", "фенек", false), // 60млекопитающие + 30пустыня = фенек
            new Element(68, "lion", "лев", false), // 60млекопитающие + 31саванна = лев
            new Element(69, "cow", "корова", false), // 60млекопитающие + 32луга = корова
            new Element(70, "rabbit", "заяц", false), // 60млекопитающие + 33болото = заяц
            new Element(71, "seal", "нерпа", true), // 60млекопитающие + 36озеро = нерпа
            new Element(72, "whale", "кит", true), // 60млекопитающие + 38океан = кит
            new Element(73, "wolf", "волк", true), // 60млекопитающие + 51лес = волк
            new Element(74, "monkey", "обезьяна", false), // 60млекопитающие + 59джунгли = обезьяна
            new Element(75, "sheep", "овца", false), // 60млекопитающие + 20гора = овца
            new Element(76, "horse", "лошадь", true), // 60млекопитающие + 44равнина = лошадь
            new Element(77, "cat", "кошка", true), // 68лев + 30пустыня = кошка
            new Element(78, "tiger", "тигр", true), // 68лев + 59джунгли = тигр
            new Element(79, "buffalo", "буйвол", true), // 69корова + 31саванна/44равнина = буйвол
            new Element(80, "fox", "лиса", true), // 67фенек + 51лес = лиса
            new Element(81, "lynx", "рысь", true), // 68лев + 51лес = рысь
            new Element(82, "snow leopard", "барс", true), // 68лев + 20гора/34снег = барс
            new Element(83, "bunny", "кролик", true), // 70заяц + 32луга = кролик
            new Element(84, "neanderthal", "неандерталец", false), // 74обезьяна + 31саванна/44равнина/20гора = неандерталец
            new Element(85, "human", "человек", false), // 84неандерталец\74обезьяна + 1огонь/66дом = человек
            new Element(86, "maid", "горничная", true), // 85человек + 66дом = служанка
            new Element(87, "witch", "ведьма", true), // 85человек + 33болото = ведьма
            new Element(88, "farmer", "фермер", true), // 85человек + 32луга = фермер
            new Element(89, "elf", "эльфийка", true), // 85человек + 51лес = эльф
            new Element(90, "savage", "дикарь", true), // 85человек + 59джунгли = дикарь
            new Element(91, "shepherd", "пастух", true), // 85человек + 20гора/75овца = пастух
            new Element(92, "mermaid", "русалка", true), // 85человек + 38океан = русалка
            new Element(93, "dragon", "дракон", true), // 58динозавр/57ящерица + 1огонь = дракон
            new Element(94, "salamander", "саламандра", true), // 57ящерица + 0вода/37река = саламандра
            new Element(95, "fly agaric", "мухомор", true), // 43гриб + 51лес = мухомор
            new Element(96, "honey mushrom", "опята", true), // 43гриб + 46дерево = опята
            new Element(97, "magnet", "магнит", true), // 54электричество + 28железо/53сталь = магнит
            new Element(98, "copper", "медь", true), // 54электричество + 13руда = медь
            new Element(99, "aluminum", "алюминий", true), // 54электричество + 9песок = алюминий
            new Element(100, "rust", "ржавчина", true), // 28железо + 0вода = ржавчина
            //new Element(101, "", false),
        };
    }
}
