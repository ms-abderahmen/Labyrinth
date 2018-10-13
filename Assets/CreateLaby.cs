using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaby : MonoBehaviour {

	public GameObject Wall;
	public GameObject Jewel;
    public GameObject Labyrinth;

    struct node {
        public int up;
        public int down;
        public int left;
        public int right;
    }

    struct position {
        public int i;
        public int j; //index
        public int side; //de 0 à 3

    }

    int[, ] verif;
    int[] steps = new int[100000];
    float jX;
    float jZ;

    position randum (node[, ] t, int n) {
        int b = 0;
        int a = 0;
        int i = 0;
        int j = 0;

        while (b == 0) {
            i = Random.Range (0, n);
            j = Random.Range (0, n);
            a = Random.Range (0, 4);

            switch (a) {
                case 0:
                    //0 = up
                    if ((i > 0) && (t[i, j].up == 0)) {
                        b = 1;
                    }
                    break;
                case 3:
                    //3 = down
                    if ((i < n - 1) && (t[i, j].down == 0)) {
                        b = 1;
                    }
                    break;
                case 1:
                    //1 = right
                    if ((j < n - 1) && (t[i, j].right == 0)) {
                        b = 1;
                    }
                    break;
                case 2:
                    //2 = left
                    if ((j > 0) && (t[i, j].left == 0)) {
                        b = 1;
                    }
                    break;
            }
        }
        position temp;
        temp.i = i;
        temp.j = j;
        temp.side = a;
        return temp;
    }

    int find (node[, ] t, int n, int i1, int j1, int i2, int j2) {
        verif[i1, j1] = 1;

        if ((i1 == i2) && (j1 == j2))
            return 1;

        if ((t[i1, j1].down == 1) && (verif[i1 + 1, j1] == 0)) {

            if (find (t, n, i1 + 1, j1, i2, j2) >= 1)
                return 1;
        }
        if ((t[i1, j1].up == 1) && (verif[i1 - 1, j1] == 0)) {

            if (find (t, n, i1 - 1, j1, i2, j2) >= 1)
                return 1;
        }
        if ((t[i1, j1].left == 1) && (verif[i1, j1 - 1] == 0)) {

            if (find (t, n, i1, j1 - 1, i2, j2) >= 1)
                return 1;
        }
        if ((t[i1, j1].right == 1) && (verif[i1, j1 + 1] == 0)) {

            if (find (t, n, i1, j1 + 1, i2, j2) >= 1)
                return 1;
        }

        return 0;

    }

    void init (node[, ] t, int n) {
        int i, j;
        for (i = 0; i < n; i++) {
            for (j = 0; j < n; j++) {
                t[i, j].up = 0;
                t[i, j].down = 0;
                t[i, j].left = 0;
                t[i, j].right = 0;
            }
        }
        // creation of verification table
        verif = new int[n, n];
    }

    void destruction (node[, ] t, int n, int i, int j, int side) {
        switch (side) {
            case 0:
                //0 = up
                if (i > 0) {
                    t[i, j].up = 1;
                    t[i - 1, j].down = 1;
                }
                break;
            case 3:
                //3 = down
                if (i < n - 1) {
                    t[i, j].down = 1;
                    t[i + 1, j].up = 1;
                }
                break;
            case 1:
                //1 = right
                if (j < n - 1) {
                    t[i, j].right = 1;
                    t[i, j + 1].left = 1;
                }
                break;
            case 2:
                //2 = left
                if (j > 0) {
                    t[i, j].left = 1;
                    t[i, j - 1].right = 1;
                }
                break;
        }
    }

    int connected (node[, ] t, int n, int i1, int j1, int i2, int j2) {
        int i, j;
        for (i = 0; i < n; i++) {
            for (j = 0; j < n; j++) {
                verif[i, j] = 0;
            }
        }
        return find (t, n, i1, j1, i2, j2);
    }

    void sorted (node[, ] maze, position[] p, int n) {
        int i = 0, max = n * (n - 1) * 2;
        position pos;
        i = 0;
        while (i < max) {
            pos = randum (maze, n);
            //printf("i: %d j: %d side: %d i : %d \n ",pos.i,pos.j,pos.side,i);
            p[i].i = pos.i;
            p[i].j = pos.j;
            p[i].side = pos.side;
            //printf("i: %d j: %d side: %d i : %d \n",p[i].i,p[i].j,p[i].side,i);
            destruction (maze, n, pos.i, pos.j, pos.side);
            i++;
        }
    }

    void afficher(node[,] t, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (t[i, j].up == 0)
                {
                    //printf("up i = %d j = %d\n",i,j);
                    //SDL_RenderDrawLine(renderer, c+gap*j, c+gap*i, c+gap*(j+1), c+gap*i);
                    (Instantiate(Wall, new Vector3(j, 0.5f, i), Quaternion.Euler(0f, 90f, 0f)) as GameObject).transform.parent = Labyrinth.transform;
                }

            }

            for (int j = 0; j < n; j++)
            {
                if (t[i, j].left == 0)
                {
                    (Instantiate(Wall, new Vector3(j, 0.5f, i), Quaternion.Euler(0f, 0f, 0f)) as GameObject).transform.parent = Labyrinth.transform;
                }
            }

        }
        for (int j = 0; j < n; j++)
        {

            (Instantiate(Wall, new Vector3(n, 0.5f, j), Quaternion.Euler(0f, 0f, 0f)) as GameObject).transform.parent = Labyrinth.transform;
        }
        for (int j = 0; j < n; j++)
        {

            (Instantiate(Wall, new Vector3(j, 0.5f, n), Quaternion.Euler(0f, 90f, 0f)) as GameObject).transform.parent = Labyrinth.transform;
        }

        jX = (float)Random.Range(0, n);
        jZ = (float)Random.Range(0, n);
        Instantiate(Jewel, new Vector3(jX + 0.5f, 0.2f, jZ + 0.5f), Quaternion.identity);
    }

    int jewelPos(node[,] t, int n, int i1, int j1, int i2, int j2,int counter,int[] steps)
    {
        verif[i1, j1] = 1;
        counter++;

        if ((i1 == i2) && (j1 == j2))
            return 1;

        if ((t[i1, j1].down == 1) && (verif[i1 + 1, j1] == 0))
        {

            if (jewelPos(t, n, i1 + 1, j1, i2, j2,counter,steps) >= 1)
            {
                steps[counter - 1] = 3;
                return 1;
            }
        }
        if ((t[i1, j1].up == 1) && (verif[i1 - 1, j1] == 0))
        {

            if (jewelPos(t, n, i1 - 1, j1, i2, j2,counter,steps) >= 1)
            {
                steps[counter - 1] = 0;
                return 1;
            }
        }
        if ((t[i1, j1].left == 1) && (verif[i1, j1 - 1] == 0))
        {

            if (jewelPos(t, n, i1, j1 - 1, i2, j2,counter,steps) >= 1)
            {
                steps[counter - 1] = 2;
                return 1;
            }
        }
        if ((t[i1, j1].right == 1) && (verif[i1, j1 + 1] == 0))
        {

            if (jewelPos(t, n, i1, j1 + 1, i2, j2,counter,steps) >= 1)
            {
                steps[counter - 1] = 1;
                return 1;
            }
        }

        counter--;
        return 0;

    }

    int jewelConnected(node[,] t, int n, int i1, int j1, int i2, int j2)
    {
        int i, j;
        for (i = 0; i < n; i++)
        {
            for (j = 0; j < n; j++)
            {
                verif[i, j] = 0;
            }
        }
        return jewelPos(t, n, i1, j1, i2, j2,0,steps);
    }

    // Use this for initialization
    void Start () {
        int n = 25;
        node[,] maze = new node[n,n];
        init(maze,n);
		position[] p = new position[n*(n-1)*2];
		sorted (maze, p, n);
		init (maze, n);

		int i1 = 0,j1 = 0;
		for (int i=0; i<n*(n-1)*2; i++)
		{
			i1=p[i].i;
			j1=p[i].j;
			switch(p[i].side)
			{
			case 0:
				// 0=up
				i1--;
				break;
			case 1:
				// 1=right
				j1 ++;
				break;
			case 2:
				//2=left
				j1--;
				break;
			case 3:
				//3=down
				i1++;
				break;
			}
			if(connected(maze,n,p[i].i,p[i].j,i1,j1) == 0)
			{
				destruction(maze,n,p[i].i,p[i].j,p[i].side);
			}
		}
		afficher (maze, n);
        jewelConnected(maze, n, 0, 0, (int)jX, (int)jZ);
        
    }

    // Update is called once per frame
    void Update () {

    }
}