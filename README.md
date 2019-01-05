# TCGA Query Generator
This Windows GUI tool generates a query from a list of TCGA ids to be used at [portal.gdc.cancer.gov/query](https://portal.gdc.cancer.gov/query). It can also directly download the manifest file instead.
## Usage
### Open
Select a file containing a list of TCGA ids. Ids can be quoted or unquoted. Only the first 12 characters of each ID is used.
#### Example
Input file:
```
TCGA-05-4250
"TCGA-05-4405"
TCGA-05-4415-0192
"TCGA-05-4417-0164"
```
### Copy Query
The query will be copied to the user's clipboard, which can be pasted at [portal.gdc.cancer.gov/query](https://portal.gdc.cancer.gov/query). Keep in mind longer queries are slow on the website; using the "Download Manifest" feature is ideal.
#### Example
Clipboard output:
```
cases.project.program.name in ["TCGA"] and cases.submitter_id in ["TCGA-05-4250","TCGA-05-4405","","TCGA-05-4415","","TCGA-05-4417"]
```
