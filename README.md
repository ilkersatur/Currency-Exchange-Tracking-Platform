# Currency Exchange Tracking Platform

### Data API

- The API saves the information on the link below in the database.
- https://www.tcmb.gov.tr/kurlar/today.xml
- Date Range must be last 2 months
- Database must be PostreSQL or mySQL
- Data Layer must be EFCore
- The API must be use swagger
- The API must be ASP.Net core
- Programming language must be C#
<div>
<image src="https://github.com/ilkersatur/Currency-Exchange-Tracking-Platform/blob/main/image/Create%20Database.gif?raw=true" width="300px"/>
&nbsp &nbsp &nbsp &nbsp &nbsp 
<image src="https://github.com/ilkersatur/Currency-Exchange-Tracking-Platform/blob/main/image/Get%20Daily%20Currency%20Data.gif?raw=true" width="300px"/>

<image src="https://github.com/ilkersatur/Currency-Exchange-Tracking-Platform/blob/main/image/Get%20Last%202%20Months%20Data.gif?raw=true" width="300px" align="right"/>
</div>

### Bussiness API

- The API fetches the requested exchange rate from the cache.
- Cache source must be the database
- The cache must not use in memory
- The API must be use swagger
- The API must be ASP.Net core
- Programming language must be C#


<image src="https://github.com/ilkersatur/Currency-Exchange-Tracking-Platform/blob/main/image/fetches%20the%20requested%20exchange%20rate%20from%20the%20cache.gif?raw=true" width="300px" align="center"/>

### Currency Web Site
- Web Interface must be ASP.Net Core MVC
- When clicking the search button, the webpage retrieves data from the Business API and
generate chart.
- Web page must be call the Business API with Ajax and use jQuery Framework.
- Chart must be generate using by d3js framework.
- Web page must be use bootstrap framework

<image src="https://github.com/ilkersatur/Currency-Exchange-Tracking-Platform/blob/main/image/Ads%C4%B1z%20tasar%C4%B1m.gif?raw=true" width="300px"/>
