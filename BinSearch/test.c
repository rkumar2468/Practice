#include "bst2dll.h"

int bstValidation(BST *root)
{
  updateMinMax(root);
  return isBST(root,MIN,MAX);
}

int isBST(BST *root, int min, int max)
{
  if(root == NULL) return 1;
  if(root->data < min || root->data > max) return -1;
  return (isBST(root->left,min,root->data) * isBST(root->right, root->data, MAX));
}

void updateMinMax(BST *root)
{
  if(root == NULL) {
    printf("Root NULL\n");
    MAX=MIN=0;
  }
  BST *temp = root;
  
  while(temp != NULL){
     MIN = temp->data;
     temp = temp->left;
  }
  while(root != NULL){
     MAX = root->data;
     root = root->right;
  }
  printf("\n %d %d == \n",MIN,MAX);
  
}

/*
In-Order Traversal using iterative method.!
GeeksforGeeks: http://www.geeksforgeeks.org/inorder-tree-traversal-without-recursion/
*/

void sortToDLL(BST **head);

void inorderTraversal(BST *head)
{
  if (head == NULL) return;
  inorderTraversal(head->left);
  printf(" %d ", head->data);
  inorderTraversal(head->right);
}

void printBST(BST *head)
{
  if(head == NULL) return;
  printf (" %d ",head->data );
  printBST(head->left);
  printBST(head->right);
    
}

void push(BST **head, int data)
{
/*
  BST *newBST = (BST *)malloc(sizeof(BST));
  newBST->left = NULL;
  newBST->right = NULL;
  newBST->data = data;
*/
  if (*head == NULL)
    {
      BST *newBST = (BST *)malloc(sizeof(BST));
      newBST->left = NULL;
      newBST->right = NULL;
      newBST->data = data;
      // MAX = (MAX < data)?data:MAX;
      // MIN = (MIN > data)?data:MIN;
      *head=newBST;
    }
  else if(((*head)->data) > data)
    push(&((*head)->left), data);
  else {
    push(&((*head)->right), data);  
  }
}

int main()
{
  // BST practice //
  BST *b;
 /*
  push(&b, 15);
  push(&b, 12);
  push(&b, 10);
  push(&b, 9);
 */
  insertRec(&b, 15); 
  insertRec(&b, 10); 
  insertRec(&b, 12); 
  insertRec(&b, 18); 
  // printBST(b);
  // printf("\n");
  // b->data = 13;
  printBST(b);
  printf("\n");
  int i;
  memset((void*)array, 0, 10);
  inordertrav(b);
  for(i=0;i<10;i++)
	printf(" %d ",array[i]);
/*
  int x = bstValidation(b);
  printf("\n MAX=%d x=%d\n",MAX, x);
  if (x == 1)
    printf ("Valid BST.!\n");
  else {
    printf("Invalid BST.!\n");
    return -1;
  }
  binarySearch(b, 14);
*/
  printf("\nInorder successor of 10 : %d \n",inorderSuccessor(b,10));
  // inOrderSuccessorPrint(b,12);
  printf("\n");
  return 0;
}


void binarySearch(BST *head, int data)
{
  if (head == NULL) {
    printf("No valid match found.!\n");
    return;
  }
  if(head->data == data){
    printf("Data Found.!\n");
    return ;
  }
  else if(head->data < data){
    binarySearch(head->right, data);
  } 
  else
    binarySearch(head->left, data);
}
