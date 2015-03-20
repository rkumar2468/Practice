#include "bst2dll.h"

void printDLL(DLL *root)
{
	while(root != NULL){
		printf(" %d <--> ",root->data);
		root = root->next;
	}
}

BST *newBST(int data)
{
	BST *temp = (BST *)malloc(sizeof(BST));
	temp->data = data;
	temp->left = NULL;
	temp->right = NULL;
	return temp;
}

// Recursion //
void insertRec(BST **head, int data)
{
	if(*head == NULL)
		*head = newBST(data);
	else {
		if((*head)->data > data)
			insertRec(&((*head)->left), data);
		else 
			insertRec(&((*head)->right), data);
	}

}	

// Iteration method //
void insert(BST **head, int data)
{
	int left = 0;
	int right = 0;
	if (*head == NULL)
	{
		BST *temp = (BST *)malloc(sizeof(BST));
		temp->data = data;
		temp->left = NULL;
		temp->right = NULL;
		*head = temp;
	}
	else 
	{
		BST *temp = *head;
		BST *prev = NULL;
		while(temp != NULL)
		{
			left = right = 0;	
			prev = temp;
			if(temp->data < data)
			{
				// To Traverse Right Tree //
				temp = temp->right;
				right = 1;
			} 
			else 
			{
				// To Traverse Left Tree //
				temp = temp->left;
				left = 1;
			}
		}
		BST *tmp = (BST *)malloc(sizeof(BST));
                tmp->data = data;
                tmp->left = NULL;
                tmp->right = NULL;
		if(left == 1)
			prev->left = tmp;
		else 
			prev->right = tmp;
	}
}

void inOrderSuccessorPrint(BST *head, int data)
{
	static int print = 0;
	if(head != NULL) {
		inOrderSuccessorPrint(head->left, data);
		if (print == 1) {
			print = 0;
			printf (" %d " , head->data);
			return;
		}
		if(head->data == data) print = 1;
		inOrderSuccessorPrint(head->right, data);
	}
}

int minvalue(BST *head)
{
	while(head->left != NULL)
		head = head->left;
	return head->data;
}

int inorderSuccessor(BST *head, int data)
{
	if (head == NULL)
	{
		return -1;
	} else {
		BST *temp = head;
		BST *prev = NULL;
		while(temp != NULL)
		{
			if(temp->data < data)
				temp = temp->right;	
			else if (temp->data > data)
				temp = temp->left;
			else 
				break;
		}
		
		if(temp->right != NULL)
		{
			return minvalue(temp->right);
		}
		while(head != NULL)
		{
			if (head->data == data)
				return (prev != NULL)?prev->data:-1;
			else if(head->data < data) {
				// prev = head;
				head = head->right;
			} else {
				prev = head;
				head = head->left;
			}
		}
	}
	return -1;
}

void inordertrav(BST *head)
{
	static int ind = 0;
	if (head == NULL ) return;
	inordertrav(head->left);
	array[ind++] = head->data;
	inordertrav(head->right);
}

