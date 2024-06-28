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
            new Element(0, "water", "����", false), // 0����
            new Element(1, "fire", "�����", false), // 1�����
            new Element(2, "stone", "������", false), // 2������
            new Element(3, "air", "������", false), // 3������
            new Element(4, "steam", "���", false), // 0���� + 1����� = ���
            new Element(5, "cley", "�����", false), // 0���� + 2������ = �����
            new Element(6, "ice", "���", false), // 0���� + 3������ = ���
            new Element(7, "lava", "����", false), // 1����� + 2������ = ����
            new Element(8, "light", "����", false), // 1����� + 3������ = ����
            new Element(9, "sand", "�����", false), // 2������ + 3������ = �����
            new Element(10, "obsidian", "��������", true), // 0���� + 7���� = ��������
            new Element(11, "dirt", "�����", false), // 0���� + 9����� = �����
            new Element(12, "ceramics", "��������", false), // 1����� + 5����� = �������� (��������)
            new Element(13, "ore", "����", false), // 7���� + 9����� = ����
            new Element(14, "comet", "������", false), // 2������ + 6��� = ������
            new Element(15, "vulcan", "������", false), // 2������ + 7���� = ������
            new Element(16, "explosion", "�����", true), // 0���� + 15������ = �����
            new Element(17, "cloud", "������", false), // 3������ + 4��� = ������
            new Element(18, "glass", "������", false), // 1����� + 9����� = ������
            new Element(19, "cement", "������", false), // 5����� + 9����� = ������
            new Element(20, "mountain", "����", false), // 6��� + 15������ = ����*
            new Element(21, "wind", "�����", false), // 3������ + 20���� = �����
            new Element(22, "rain", "�����", false), // 0���� + 17������ = �����
            new Element(23, "thunderstorm", "�����", false), // 21����� + 22����� = �����
            new Element(24, "rainbow", "������", true), // 8���� + 22����� = ������
            new Element(25, "asteroid", "��������", false), // 13���� + 14������ = ��������
            new Element(26, "meteorite", "��������", false), // 3������/27����� + 25��������/14������ = ��������
            new Element(27, "earth", "�����", false), // 5����� + 11����� = �����
            new Element(28, "iron", "������", false), // 13���� + 27����� = ������
            new Element(29, "lightning", "������", false), // 23����� + 28������ = ������
            new Element(30, "desert", "�������", false), // 8���� + 9����� = �������
            new Element(31, "savannah", "�������", false), // 0����/22����� + 30������� = �������
            new Element(32, "grassland", "����", false), // 0����/22����� + 27����� = ����
            new Element(33, "swamp", "������", false), // 22����� + 32���� = ������
            new Element(34, "snow", "����", false), // 6��� + 22����� = ����
            new Element(35, "crater", "������", false), // 26�������� + 32����/31�������/30������� = ������
            new Element(36, "lake", "�����", false), // 0����/22����� + 35������ = �����
            new Element(37, "river", "����", false), // 0����/22����� + 20���� = ����
            new Element(38, "ocean", "�����", false), // 36����� + 37���� = �����
            new Element(39, "salt", "����", true), // 1�����/30������� + 38����� = ����
            new Element(40, "bacteria", "��������", false), // 8���� + 38����� = �����(��������)
            new Element(41, "moss", "���", false), // 27�����/32����/31������� + 40����� = ��������(���)
            new Element(42, "clam", "�������", false), // 0����/38����� + 40����� = ��������(�������)
            new Element(43, "mushroom", "����", false), // 33������ + 40����� = ����(�������)
            new Element(44, "plains", "�������", false), // 8����/31������� + 32���� = �������
            new Element(45, "cactus", "������", true), // 41�������� + 30������� = ������
            new Element(46, "tree", "������", false), // 41�������� + 31������� = ������
            new Element(47, "flower", "������", true), // 41�������� + 32���� = ������
            new Element(48, "berry", "�����", true), // 41�������� + 33������ = �����
            new Element(49, "seaweed", "���������", true), // 41�������� + 36����� = ���������
            new Element(50, "�oral", "�����", true), // 41�������� + 38����� = ������
            new Element(51, "forest", "���", false), // 46������ + 32���� = ���
            new Element(52, "coal", "�����", false), // 46������ + 1����� = �����
            new Element(53, "steel", "�����", false), // 52����� + 28������ = �����
            new Element(54, "electricity", "�������������", false), // 53����� + 29������ = �������������
            new Element(55, "fish", "����", false), // 42�������� + 36�����/38����� = ����
            new Element(56, "frog", "�������", false), // 42��������/55���� + 33������ = �������
            new Element(57, "lizard", "�������", false), // 42��������/56����������� + 30������� = �������
            new Element(58, "dinosaur", "���������", false), // 42��������/57�������������� + 31�������/32���� = ���������
            new Element(59, "jungle", "�������", false), // 51��� + 0����/22����� = �������
            new Element(60, "mouse", "�����", false), // 58��������� + 26��������/15������ = �����
            new Element(61, "concrete", "�����", false), // 19������ + 0���� = �����
            new Element(62, "brick", "������", false), // 19������ + 12�������� = ������
            new Element(63, "foundation", "���������", false), // 61����� + 53����� = ��������� (��� 1/4)
            new Element(64, "wall", "�����", false), // 63��������� + 62������ = ����� (��� 2/4)
            new Element(65, "window", "����", false), // 64����� + 18������ = ���� (��� 3/4)
            new Element(66, "house", "���", false), // 65���� + 12�������� = ���
            new Element(67, "fenech", "�����", false), // 60������������� + 30������� = �����
            new Element(68, "lion", "���", false), // 60������������� + 31������� = ���
            new Element(69, "cow", "������", false), // 60������������� + 32���� = ������
            new Element(70, "rabbit", "����", false), // 60������������� + 33������ = ����
            new Element(71, "seal", "�����", true), // 60������������� + 36����� = �����
            new Element(72, "whale", "���", true), // 60������������� + 38����� = ���
            new Element(73, "wolf", "����", true), // 60������������� + 51��� = ����
            new Element(74, "monkey", "��������", false), // 60������������� + 59������� = ��������
            new Element(75, "sheep", "����", false), // 60������������� + 20���� = ����
            new Element(76, "horse", "������", true), // 60������������� + 44������� = ������
            new Element(77, "cat", "�����", true), // 68��� + 30������� = �����
            new Element(78, "tiger", "����", true), // 68��� + 59������� = ����
            new Element(79, "buffalo", "������", true), // 69������ + 31�������/44������� = ������
            new Element(80, "fox", "����", true), // 67����� + 51��� = ����
            new Element(81, "lynx", "����", true), // 68��� + 51��� = ����
            new Element(82, "snow leopard", "����", true), // 68��� + 20����/34���� = ����
            new Element(83, "bunny", "������", true), // 70���� + 32���� = ������
            new Element(84, "neanderthal", "������������", false), // 74�������� + 31�������/44�������/20���� = ������������
            new Element(85, "human", "�������", false), // 84������������\74�������� + 1�����/66��� = �������
            new Element(86, "maid", "���������", true), // 85������� + 66��� = ��������
            new Element(87, "witch", "������", true), // 85������� + 33������ = ������
            new Element(88, "farmer", "������", true), // 85������� + 32���� = ������
            new Element(89, "elf", "��������", true), // 85������� + 51��� = ����
            new Element(90, "savage", "������", true), // 85������� + 59������� = ������
            new Element(91, "shepherd", "������", true), // 85������� + 20����/75���� = ������
            new Element(92, "mermaid", "�������", true), // 85������� + 38����� = �������
            new Element(93, "dragon", "������", true), // 58��������/57������� + 1����� = ������
            new Element(94, "salamander", "����������", true), // 57������� + 0����/37���� = ����������
            new Element(95, "fly agaric", "�������", true), // 43���� + 51��� = �������
            new Element(96, "honey mushrom", "�����", true), // 43���� + 46������ = �����
            new Element(97, "magnet", "������", true), // 54������������� + 28������/53����� = ������
            new Element(98, "copper", "����", true), // 54������������� + 13���� = ����
            new Element(99, "aluminum", "��������", true), // 54������������� + 9����� = ��������
            new Element(100, "rust", "��������", true), // 28������ + 0���� = ��������
            //new Element(101, "", false),
        };
    }
}
