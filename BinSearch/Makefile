CFLAGS= 
DEBUG=-ggdb -g -gdwarf-2
GCC=/usr/bin/gcc

all:	BinarySearch

BinarySearch:	bins.c
	$(GCC) $(CFLAGS) bins.c -o bin/BinarySearch

debug:
	$(GCC) $(CFLAGS) $(DEBUG) bins.c -o bin/BinarySearch
	
clean:
	rm -rf bin/*.o bin/BinarySearch 
