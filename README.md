# PaoloAlto_PermutationService


Searching for permutations of a user input word in a local DB file. 

<br/>
<b>Permutation definition:</b> a string is a permutation of some characters, if all string characters are contained in the list of the permutation characters.

<br/>
<br/>
Algorithm description -
<br/>

-Accept a string and store the characters as permutation character array A.<br/>
-The length L of the array gives the length of the entries to match in the dictionary.<br/>
-Read the dictionary into a hash table D for all entries E of D<br/>
-Print only those Es of length L an where all chars of E are contained in A<br/>
<br/>

* Before starting the search, the input word is convereted to lower-case.

<br/>


<b>*KNOWN ISSUES</b>

-No UI updates on long words being permutated.<br/>
-The Test project files are included but the project was removed from the solution
since it was causing build issues when downloading/cloning from Github.</br>
-The actual urls are longer since I used the built-in description page for showing the results
and this page takes parameters regarding the specific tab to open in.
<br/>



<b>INSTALLATION OF MONO</b>
<br/>

1st step, Adding the repo, paragraph 1:

https://www.mono-project.com/download/stable/#download-lin
I used Ubuntu 20.04

2nd step, install Mono, paragraph 2:

https://www.mono-project.com/download/stable/#download-lin

<br/>

<b>Running the web-service:</b>

In terminal: cd to the project files folder (not the .sln file) and run:
xsp4 --port 8000


for example: "Projects/Permutation_Services/Permutation_Services"


once there, run: "xsp4 --port 8000"

<b>In case of any issues/error messages, run:</b>
"sudo lsof -i:8000 -n -P" in terminal - to check if something is using port 8000
(if so then kill it and re-run "xsp4 --port 8000 --verbose").


<b>* Don't forget to build the source code on RELEASE after cloning/downloading it. I used
MonoDevelop.</b>


open a web browser and paste the address: 

http://127.0.0.1:8000 OR http://localhost:8000

--or

http://localhost:8000/api/v1/similar.asmx/Find_Permutations_In_DB?word=door
<br/>
http://127.0.0.1:8000/api/v1/similar.asmx/Find_Permutations_In_DB?word=apple
<br/>
<br/>
http://localhost:8000/api/v1/stats.asmx/Show_Stats
<br/>
http://127.0.0.1:8000/api/v1/stats.asmx/Show_Stats
<br/>


<b><h2>Press "Back" after viewing the results.</h2></b>
<br/>
<br/>
You can unzip the Compiled_Binaries.zip archive and place the \bin and \obj folders next to the source files
to skip building the project yourself. After running "xsp4 --port 8000 --verbose", use the links above.








