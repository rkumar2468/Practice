#include<stdio.h>
#include<string.h>

int MAX, MIN;

typedef struct node BST;
typedef struct nodeDLL DLL;

struct node{
	int data;
	BST *left;
	BST *right;
};

struct nodeDLL{
	int data;
	DLL *prev;
	DLL *next;
};
int array[10];
// memset((void*)array, 0, MAX);

void binarySearch(BST *head, int data);
void updateMinMax(BST *head);
int bstValidation(BST *head);
int isBST(BST *root, int min, int max);
void inorderTraversal(BST *head);
void printBST(BST *head);
void push(BST **head, int data);
void sort2DLL(BST *head, DLL *node);
void printDLL(DLL *node);
int inorderSuccessor(BST *head, int data);
void inOrderSuccessorPrint(BST *head, int data);
void insert(BST **head, int data);
void insertRec(BST **head, int data);
BST *newBST(int data);
void inordertrav(BST *head);
