using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int[][] boardMove;

	// Use this for initialization
	void Start () {
        boardMove = new int[112][];
        FillBoardMove();

    }
	
	// Update is called once per frame
	void Update () {
        //boardMove[0] = new int[] { 1, 2 };
    }
    void FillBoardMove()
    {
        boardMove[0] = new int[] {2, 3};
        boardMove[1] = new int[] {1, 5};
        boardMove[2] = new int[] {1, 4};
        boardMove[3] = new int[] {3, 23};
        boardMove[4] = new int[] {2, 8};
        boardMove[5] = new int[] {7, 8};
        boardMove[6] = new int[] {6, 10};
        boardMove[7] = new int[] {5, 6, 9};
        boardMove[8] = new int[] {8, 24};
        boardMove[9] = new int[] {7, 11};

        boardMove[10] = new int[] {10, 12, 13};
        boardMove[11] = new int[] {11,14};
        boardMove[12] = new int[] {11,25};
        boardMove[13] = new int[] {12,15};
        boardMove[14] = new int[] {14,16,17};
        boardMove[15] = new int[] {15,19};
        boardMove[16] = new int[] {15,18};
        boardMove[17] = new int[] {17,26};
        boardMove[18] = new int[] {16,20};
        boardMove[19] = new int[] {19,21};

        boardMove[20] = new int[] {20,22};
        boardMove[21] = new int[] {21,27};
        boardMove[22] = new int[] {4,28};
        boardMove[23] = new int[] {9,30,31};
        boardMove[24] = new int[] {33,34,13};
        boardMove[25] = new int[] {36,18,37};
        boardMove[26] = new int[] {22,39};
        boardMove[27] = new int[] {23,29};
        boardMove[28] = new int[] {28,30,40};
        boardMove[29] = new int[] {24,29};

        boardMove[30] = new int[] {24,32};
        boardMove[31] = new int[] {31,33,42};
        boardMove[32] = new int[] {25,32};
        boardMove[33] = new int[] {25,35};
        boardMove[34] = new int[] {34,36,47};
        boardMove[35] = new int[] {26,35};
        boardMove[36] = new int[] {26,38};
        boardMove[37] = new int[] {37,39,53};
        boardMove[38] = new int[] {27,38};
        boardMove[39] = new int[] {29,41};

        boardMove[40] = new int[] {40,45};
        boardMove[41] = new int[] {32,43};
        boardMove[42] = new int[] {42,44,55};
        boardMove[43] = new int[] {43,46};
        boardMove[44] = new int[] {41,46};
        boardMove[45] = new int[] {44,45,61};
        boardMove[46] = new int[] {35,48};
        boardMove[47] = new int[] {47,49,54};
        boardMove[48] = new int[] {48,50};
        boardMove[49] = new int[] {49,51,57};

        boardMove[50] = new int[] {50,52};
        boardMove[51] = new int[] {51, 53};
        boardMove[52] = new int[] {38, 52};
        boardMove[53] = new int[] {48, 56};
        boardMove[54] = new int[] {43, 56};
        boardMove[55] = new int[] {54, 55};
        boardMove[56] = new int[] {50, 58};
        boardMove[57] = new int[] {57,59};
        boardMove[58] = new int[] {58, 60};
        boardMove[59] = new int[] {59,67, 71};// dodawałem

        boardMove[60] = new int[] {46, 62};
        boardMove[61] = new int[] {61, 63};
        boardMove[62] = new int[] {62, 64};
        boardMove[63] = new int[] {63,65, 74};// dodawałem
        boardMove[64] = new int[] {64, 66};
        boardMove[65] = new int[] {65, 67};
        boardMove[66] = new int[] {60, 66};
        boardMove[67] = new int[] {69, 72, 75};
        boardMove[68] = new int[] {68, 70};
        boardMove[69] = new int[] {69, 71};

        boardMove[70] = new int[] {60, 70};
        boardMove[71] = new int[] {68, 73};
        boardMove[72] = new int[] {72, 74};
        boardMove[73] = new int[] {64, 73};
        boardMove[74] = new int[] {68, 76};
        boardMove[75] = new int[] {75, 77, 96};
        boardMove[76] = new int[] {76, 78};
        boardMove[77] = new int[] {77, 79};
        boardMove[78] = new int[] {78, 80};
        boardMove[79] = new int[] {79, 81};

        boardMove[80] = new int[] {80, 82};
        boardMove[81] = new int[] {81, 83};
        boardMove[82] = new int[] {82, 84};
        boardMove[83] = new int[] {83, 85};
        boardMove[84] = new int[] {84, 86};
        boardMove[85] = new int[] {85, 87};
        boardMove[86] = new int[] {86, 88};
        boardMove[87] = new int[] {87, 89};
        boardMove[88] = new int[] {88, 90};
        boardMove[89] = new int[] {89, 91};

        boardMove[90] = new int[] {90, 92};
        boardMove[91] = new int[] {91, 93};
        boardMove[92] = new int[] {92, 94};
        boardMove[93] = new int[] {93, 95, 112};
        boardMove[94] = new int[] {999};
        boardMove[95] = new int[] {76, 97};
        boardMove[96] = new int[] {96, 98};
        boardMove[97] = new int[] {97, 99};
        boardMove[98] = new int[] {98, 100};
        boardMove[99] = new int[] {99, 101};

        boardMove[100] = new int[] {100, 102};
        boardMove[101] = new int[] {101, 103};
        boardMove[102] = new int[] {102, 104};
        boardMove[103] = new int[] {103, 105};
        boardMove[104] = new int[] {104, 106};
        boardMove[105] = new int[] {105, 107};
        boardMove[106] = new int[] {106, 108};
        boardMove[107] = new int[] {107, 109};
        boardMove[108] = new int[] {108, 110};
        boardMove[109] = new int[] {109, 111};

        boardMove[110] = new int[] {110, 112};
        boardMove[111] = new int[] {94, 111};
        //boardMove[112] = new int[] { };

    }
}
