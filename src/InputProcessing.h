#ifndef AP_EX1_INPUTPROCESSING_H
#define AP_EX1_INPUTPROCESSING_H

#include <iostream>
#include <algorithm>
#include "TwoDim.h"
#include "Point2D.h"
#include "NodePoint.h"
#include "Bfs.h"
#include "TaxiCenter.h"
#include "Socket.h"
#include "ThreadPool.h"

//this header declares about the helping funcs in the main- that process the inputs and the menu
/**
 * @param input is the input from the user, "sizeX_sizeY,startX_startY,endX,endY"
 * @param separatedStrings is the vector that we fill with the separated strings
 * @param separator is the char that separates the parameters
 * @return a pointer to a string arr which contains the separated strings
 */
void separateString(string input, vector<string> &separatedStrings, char separator);
/**
 * @param inputOfPoint is the string we want to find it's num of members
 * @param separator is the char that separates the parameters
 * @return the ammount of '_' which determine the dim of the grid +1
 * the dim will be the ammount of '_' + 1.
 */
int countMembers(string inputOfPoint, char separator);
/**
 * the func checks the validation of the cab string
 * @param separatedMembers is the members of the cab- as strings
 * @return -1 if the input isn't valid, else return 0
 */
int cabInputProcessing(vector<string> &separatedMembers);
/**
 * checks if the num is greater than a lower bound
 * @param num the num to check
 * @param lowerBound the num that we compare to
 * @return true if num>=lower bound, else return false
 */
bool ifGreaterThan(int num, int lowerBound);
/**
 * checks if string is a num
 * @param str the str to check
 * @return true if it's a num, else false
 */
bool ifStringIsNum(string str);
/**
 * checks the valsidation of a driver string
 * @param separatedMembers is the members of the cab- as strings
 * @return -1 if the input isn't valid, else return 0
 */
int driverInputProcessing(vector<string> &separatedMembers);
/**
 * the func gets from the user- the input for the the grid and the obstacles.
 * it checks the validaion
 * @return the new grid inialized with obstacles - if the input was correct
 * if not- return NULL
 */
Grid* createGridAndObstacles();
/**
 * the func gets from user the num of clients, and checks the validation of the input
 * @return -1 if the input isn't valid, else return 0
 */
int getNumOfClients();
/**
 * the func operates the menu and is called from the main
 * @param station the taxi center
 */
void menu(TaxiCenter* station, Socket* udp);
/**
 * the func calls the method "addNewDriver" from the TaxiCenter Classs
 * @param station the taxi center
 */
void insertDriver(TaxiCenter* station, Socket* udp, int newClientSd);
/**
 * the func calls the method "addNewTrip" from the TaxiCenter Classs
 * @param station the taxi center
 */
void insertTrip(TaxiCenter* station, ThreadPool *pool);
/**
 * the func calls the method "addNewCab" from the TaxiCenter Classs
 * @param station the taxi center
 */
void insertCab(TaxiCenter* station);
/**
 * the func calls the method "findDriverLocationById" from the TaxiCenter Class and prints
 * the point of the driver's location
 * @param station the taxi center
 */
void driverLocationRequest(TaxiCenter* station);
/**
 * @param buffer the buffer to convert to a string
 * @param bufflen the length of the buffer
 * @return a new string
 */
string bufferToString(char* buffer, int bufflen);
/**
 * that func is sent to a new thread. the thread is in charge of the connection with a client
 * and inserts a new driver and send/recieve locations.
 * @param element get ClientData Struct
 * @return void
 */
void *manageClient(void* element);
/**
 * checks the trip validation
 * @param separatedMembers is the members of the cab- as strings
 * @param mapSize - the point that indicates the grid size (xGridSize,yGridSize)
 * @return -1 if the input isn't valid, else return 0
 */
int tripInputProcessing(vector<string> &separatedMembers, Point* mapSize);
/**
 * the struct to sent to a thread
 */
struct ClientData {
    TaxiCenter* station;
    Socket *tcp;
    int clientSd;
    int index;
};

#endif //AP_EX1_INPUTPROCESSING_H