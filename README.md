# ScamNumberSearch
This project is aimed at extracting the phone numbers of tech support scammers that are specifically abusing website SEO to push their scam numbers very high up in the search results.
This application pulls 100 results every 24 hours based on Google's free API limitations. If you want it to pull the information exactly every 24 hours, it is as simple as creating a scheduled task to do so.

### API Key
You will need to create your own Google API key in order to use this application. This process is very straightforward and can be found in Google's Documentation.

### Custom Search Engine
You will also need to create a custom search engine on the Google Developer Platform. This is about as easy as creating the API Key. I highly recommend that you target a specific site with each instance you run for categorization purposes, but it is not a requirement. You can search using the same dork for any website.

### Modification of the dork
The dork is hardcoded but can be modified quite easily. I'll leave that up to the reader to modify the dork as they see fit. I may also add command line parameters for a more on-the-fly approach at a later time.
