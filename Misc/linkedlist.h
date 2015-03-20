#include<stdio.h>

typedef struct ll Node;

struct ll{
	int data;
	Node *next;
};

void insert(Node **head, int data);
void removell(Node **head, int data);
void reverse(Node **head);
void printLL(Node *head);
Node *newLL(int data);
void insertSorted(Node **head, int data);
void addNumToLL(Node **head, int data, int sum);
void facebookRec(Node **head);
void reverseUpdt(Node **head);
