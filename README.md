[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![Pulls][pulls-shield]][pulls-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">

  <h1 align="center">random.IT</h1>

  <p align="center">
    random.IT is a system focusing on sharing random or pseudo-random data via API.
    <br />
    <br />
    <a href="https://github.com/matdon90/random.IT/issues">Report Bug</a>
    ·
    <a href="https://github.com/matdon90/random.IT/issues">Request Feature</a>
  </p>
</p>

<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
* [Used Technologies](#used-technologies)
* [Getting Started](#getting-started)
* [Api Documentation](#api-documentation)
* [Contributing](#contributing)
* [Contact](#contact)



<!-- ABOUT THE PROJECT -->
## About The Project

random.IT is system focused on sharing random or pseudo-random data via API. The application is plan to consist of parts that generate independent data. First, it will be a GUID number generator, and then random IP protocol configurations compliant with the requirements specified in the query. The next parts, and also the next generators, will be added according to the needs or suggestions of users.


<!-- USED TECHNOLOGIES -->
### Used Technologies

Backend
* [ASP.NET Core 5.0](https://docs.microsoft.com/pl-pl/aspnet/core/?view=aspnetcore-5.0)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [AutoWrapper](https://github.com/proudmonkey/AutoWrapper)
* [Swashbuckle](https://docs.microsoft.com/en-US/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio)
* [NUnit](https://nunit.org/)
* [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
* [FluentAssertion](https://fluentassertions.com/)


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps:

* Download the latest stable version from the download tab and unzip it to your folder
* Open the solution in Visual Studio 2019. 
* Clean solution.
* Build the solution to install Nuget packages.
* Run application
* Fire up your browser and open url `https://localhost:44386/` to use a Swagger UI
* Enjoy ;-)

<!-- API DOCUMENTATION -->
## API Documentation

The API currently contains 2 endpoints: Guid and Network Configurations.
When new endpoints are created, they will be successively added to the documentation.

### GUID

| Command    | Method | Route                   | Description                                             |
|------------|--------|-------------------------|---------------------------------------------------------|
| SingleGuid | GET    | /api/guid               | Return single GUID generated based on current timedate  |

API command SingleGuid allows user to retrieve single version 1 GUID.
User can parametrize request:
* isUppercase (optional) - will return GUID in uppercase, i.e. `/api/guid?isuppercase=true`

Response data will be in the following format:
```
{
  "statusCode": <int>,
  "message": <string>",
  "result": <string>
}
```

Example response for `/api/guid?isuppercase=true`:
```json
{
  "statusCode": 200,
  "message": "GET Request successful.",
  "result": "2E0ACB1C-3B88-11EB-9410-3F26C08F1611"
}
```


| Command    | Method | Route                   | Description                                             |
|------------|--------|-------------------------|---------------------------------------------------------|
| ListGuid   | GET    | /api/guid/{guidNumbers} | Returns list with number of GUIDs declared in {guidNumbers} parameter |

API command ListGuid allows user to retrieve list of version 4 GUIDs.
Response is paginated:
* its default page size is 10 and cannot be higher than 30
* default page number is 1 and cannot be lower than 1
These can be changed in request.

User can parametrize request:
* PageNumber (default: 1) - will returns chosen page number, i.e. `/api/guid/50?PageNumber=2`
* PageSize (default: 10) - will set size of page of response, i.e. `/api/guid/50?PageSize=4`
* isUppercase (optional) - will return list of GUIDs in uppercase, i.e. `/api/guid/8?isuppercase=true`

Response data will be in the following format:
```
{
  "statusCode": <int>,
  "message": <string>,
  "result": [
    <string>
    ...
    <string>
  ],
  "pagination": {
    "pageNumber": <int>,
    "pageSize": <int>,
    "totalPages": <int>,
    "totalRecords": <int>,
    "firstPage": <string>,
    "lastPage": <string>,
    "nextPage": <string>,
    "previousPage": <string>
  }
}
```

Example response for `/api/guid/120?PageNumber=3&PageSize=15&isUppercase=true`:
```json
{
  "statusCode": 200,
  "message": "GET Request successful.",
  "result": [
    "2BC7C2B0-D254-4272-9166-8369DB9421E5",
    "959754B4-4E69-429C-9166-C7A0E260B8DB",
    "A0F64567-EFCD-450B-9166-576B0FDE3692",
    "F7F0CA44-D458-4497-9166-D01771D590AE",
    "F44826FB-961A-4E6B-9166-733AC731D9EF",
    "8B862CFD-0918-4CBD-9166-B0EA08E34423",
    "F72FDE75-5C5F-40C9-9166-221BC39F2706",
    "2630B557-A7FA-4447-9166-90E5BD87F0FE",
    "36EB5E76-4752-44C8-9166-0BCFD27D26CD",
    "78C86B09-B6E1-4AA5-9166-BAE2E99CDBD1",
    "D73A4A4D-CA6C-44B9-9166-2F8C4DDF3EC3",
    "7D5361AA-7A81-459A-9166-1C44A53A587C",
    "B64D09A4-F76C-4046-9166-94FA115B1344",
    "012B3B3B-E278-494F-9166-8E6C1A613628",
    "3232F62D-3C71-4620-9166-3F9673467CE9"
  ],
  "pagination": {
    "pageNumber": 3,
    "pageSize": 15,
    "totalPages": 8,
    "totalRecords": 120,
    "firstPage": "https://localhost:44386/api/guid/120?pageNumber=1&pageSize=15",
    "lastPage": "https://localhost:44386/api/guid/120?pageNumber=8&pageSize=15",
    "nextPage": "https://localhost:44386/api/guid/120?pageNumber=4&pageSize=15",
    "previousPage": "https://localhost:44386/api/guid/120?pageNumber=2&pageSize=15"
  }
}
```

### NETWORK CONFIGURATIONS

| Command    | Method | Route                   | Description                                             |
|------------|--------|-------------------------|---------------------------------------------------------|
| ListNetworkConfigsWithIpTemplate   | GET    | /api/networkconfig/{networkConfigsNumber} | Return list with number of random network configs declared in parameter. Can be parametrized with use of IP address template or/and subnet mask. |

API command ListNetworkConfigsWithIpTemplate allows user to retrieve list of network configs that contains:
* host IP V4 address,
* subnet mask,
* subnet address,
* subnet broadcast address,
* number of still free hosts in subnet

Response is paginated:
* its default page size is 10 and cannot be higher than 30
* default page number is 1 and cannot be lower than 1
These can be changed in request.

User can parametrize request:
* PageNumber (default: 1) - will returns chosen page number, i.e. `/api/networkconfig/50?PageNumber=2`
* PageSize (default: 10) - will set size of page of response, i.e. `/api/networkconfig/50?PageSize=4`
* ipTemplate (optional) - will return list of network confis with ip based on provided template, i.e. `/api/networkconfig/8?ipTemplate=192.168.x.x`
* subnetMask (optional) - will return list of network confis with provided subnet mask, i.e. `/api/networkconfig/8?subnetMask=255.255.255.0`

Response data will be in the following format:
```
{
  "statusCode": <int>,
  "message": <string>,
  "result": [
    {
      "ipHostAddress": <string>,
      "subnetMask": <string>,
      "subnetAddress": <string>,
      "subnetBroadcastAddress": <string>,
      "freeHostsNumberInSubnet": <int>
    }
    ...
    {
      "ipHostAddress": <string>,
      "subnetMask": <string>,
      "subnetAddress": <string>,
      "subnetBroadcastAddress": <string>,
      "freeHostsNumberInSubnet": <int>
    }
  ],
  "pagination": {
    "pageNumber": <int>,
    "pageSize": <int>,
    "totalPages": <int>,
    "totalRecords": <int>,
    "firstPage": <string>,
    "lastPage": <string>,
    "nextPage": <string>,
    "previousPage": <string>
  }
}
```

Example response for `/api/networkconfig/3?ipTemplate=192.168.x.x&subnetMask=255.255.255.0`:
```json
{
  "statusCode": 200,
  "message": "GET Request successful.",
  "result": [
    {
      "ipHostAddress": "192.168.51.1",
      "subnetMask": "255.255.255.0",
      "subnetAddress": "192.168.51.0",
      "subnetBroadcastAddress": "192.168.51.15",
      "freeHostsNumberInSubnet": 251
    },
    {
      "ipHostAddress": "192.168.51.2",
      "subnetMask": "255.255.255.0",
      "subnetAddress": "192.168.51.0",
      "subnetBroadcastAddress": "192.168.51.15",
      "freeHostsNumberInSubnet": 251
    },
    {
      "ipHostAddress": "192.168.51.3",
      "subnetMask": "255.255.255.0",
      "subnetAddress": "192.168.51.0",
      "subnetBroadcastAddress": "192.168.51.15",
      "freeHostsNumberInSubnet": 251
    }
  ],
  "pagination": {
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1,
    "totalRecords": 3,
    "firstPage": "https://localhost:44386/api/networkconfig/3?pageNumber=1&pageSize=10",
    "lastPage": "https://localhost:44386/api/networkconfig/3?pageNumber=1&pageSize=10"
  }
}
```

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. 🍴 Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. 🔃 Open a Pull Request


<!-- CONTACT -->
## Contact

Mateusz Donhefner

* Project Link: [https://github.com/matdon90/random.IT](https://github.com/matdon90/random.IT)

### Project's main contributors:

<a href="https://github.com/matdon90/random.IT/graphs/contributors">
  <img src="https://contributors-img.web.app/image?repo=matdon90/random.IT" />
</a>

<!-- Made with [contributors-img](https://contributors-img.web.app). -->

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/matdon90/random.IT.svg?style=flat-square
[contributors-url]: https://github.com/matdon90/random.IT/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/matdon90/random.IT.svg?style=flat-square
[forks-url]: https://github.com/matdon90/random.IT/network/members
[stars-shield]: https://img.shields.io/github/stars/matdon90/random.IT.svg?style=flat-square
[stars-url]: https://github.com/matdon90/random.IT/stargazers
[issues-shield]: https://img.shields.io/github/issues/matdon90/random.IT.svg?style=flat-square
[issues-url]: https://github.com/matdon90/random.IT/issues
[pulls-shield]: https://img.shields.io/github/issues-pr/matdon90/random.IT.svg?style=flat-square
[pulls-url]: https://github.com/matdon90/random.IT/pulls
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mateusz-donhefner/
