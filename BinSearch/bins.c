#include<stdio.h>
#include<stdlib.h>

struct searchBin {
	int val;
	struct searchBin *right;
	struct searchBin *left;
};

typedef struct searchBin BinSearch;

// Create a new Node //
BinSearch *createNode()
{
	BinSearch *ret = (BinSearch *)malloc(sizeof(BinSearch));
	ret->val = 0;
	ret->right=NULL;
	ret->left=NULL;
	return ret;
}

// Recursive Insert Method //
int insert(BinSearch *root, int val)
{
	int ret = 0;
		if(root->val == 0) {
			printf("1.1\n");
			root->val = val;
			return val;
		}
		else if(root->val > val) {
			printf("2\n");
			if(root->left == NULL) 
				root->left = createNode();
			insert(root->left, val);
		}
		else if(root->val < val)
		{
			printf("3\n");
			if(root->right == NULL) 
				root->right = createNode();
			insert(root->right, val);
		}
		ret = val;
	return ret;
}

// Recursive Search Algorithm //
int search(BinSearch *b, int val)
{
	int ret = -1;
	if(b == NULL) { 
		printf("Null Value \n");
		return ret;
	}
	if(b->val == val) 
		return val;
	else if (b->val > val)
		ret = search(b->left, val);
	else
		ret = search(b->right, val);
	return ret;
}

// Iterative Search Algorithm //
int searchIter(BinSearch *b, int val)
{
	int ret = -1;
	BinSearch *temp = NULL;
	if(b == NULL) { 
		printf("Null Value \n");
		return ret;
	}
	if(b->val == val) 
		return val;
	else  {
		temp = b;
		while(temp != NULL) {
			if (temp->val == val) {
				ret = val;
				break;
			} else if (temp->val > val)
				temp = temp->left;
			else
				temp = temp->right;
		}
	}
	return ret;
}

int main(void)
{
	BinSearch *bi = createNode(); 
	int ret,a;
	printf("Inserting 4\n"); 
	insert(bi,4);
	printf("Inserting 40\n"); 
	insert(bi,40);
	printf("Inserting 3\n"); 
	insert(bi,3);
	printf("Inserting 31\n"); 
	insert(bi,31);
	printf("Enter the element to be searched: \n");
	scanf("%d",&a);
	ret = searchIter(bi,a);
	if(ret < 0)
		printf("No such element in the binary tree %d\n",ret);
	else 
		printf("Value %d found in the tree.!!\n",a);
	return 0;
}	
