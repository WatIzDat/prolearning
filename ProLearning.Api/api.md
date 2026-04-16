<!-- Generator: Widdershins v4.0.1 -->

<h1 id="prolearning-api-v1">ProLearning.Api | v1 v1.0.0</h1>

> Scroll down for code samples, example requests and responses. Select a language for code samples from the tabs above or the mobile navigation menu.

Base URLs:

* <a href="http://localhost:5075/">http://localhost:5075/</a>

<h1 id="prolearning-api-v1-learningactivityendpoints">LearningActivityEndpoints</h1>

## CreateLearningActivities

<a id="opIdCreateLearningActivities"></a>

> Code samples

```http
POST http://localhost:5075/learningactivities HTTP/1.1
Host: localhost:5075
Content-Type: application/json
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    
    /// Make a dummy request
    public async Task MakePostRequest()
    {
      string url = "http://localhost:5075/learningactivities";
      
      string json = @"[
  {
    ""name"": ""string"",
    ""url"": ""string"",
    ""educationLevels"": [
      ""string""
    ],
    ""interestAreaScoreBoosts"": [
      {
        ""interestArea"": ""string"",
        ""skillLevelScoreBoosts"": [
          {
            ""skillLevel"": 0,
            ""score"": 0
          }
        ]
      }
    ],
    ""goalScoreBoosts"": [
      {
        ""goal"": ""string"",
        ""score"": 0
      }
    ]
  }
]";
      LearningActivityDto content = JsonConvert.DeserializeObject(json);
      await PostAsync(content, url);
      
      
    }

    /// Performs a POST Request
    public async Task PostAsync(LearningActivityDto content, string url)
    {
        //Serialize Object
        StringContent jsonContent = SerializeObject(content);

        //Execute POST request
        HttpResponseMessage response = await Client.PostAsync(url, jsonContent);
    }
    
    
    
    /// Serialize an object to Json
    private StringContent SerializeObject(LearningActivityDto content)
    {
        //Serialize Object
        string jsonObject = JsonConvert.SerializeObject(content);

        //Create Json UTF8 String Content
        return new StringContent(jsonObject, Encoding.UTF8, "application/json");
    }
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`POST /learningactivities`

> Body parameter

```json
[
  {
    "name": "string",
    "url": "string",
    "educationLevels": [
      "string"
    ],
    "interestAreaScoreBoosts": [
      {
        "interestArea": "string",
        "skillLevelScoreBoosts": [
          {
            "skillLevel": 0,
            "score": 0
          }
        ]
      }
    ],
    "goalScoreBoosts": [
      {
        "goal": "string",
        "score": 0
      }
    ]
  }
]
```

<h3 id="createlearningactivities-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[LearningActivityDto](#schemalearningactivitydto)|true|none|

> Example responses

> 400 Response

```json
{
  "type": null,
  "title": null,
  "status": null,
  "detail": null,
  "instance": null,
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

<h3 id="createlearningactivities-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|204|[No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)|No Content|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[ProblemDetails](#schemaproblemdetails)|

## GetLearningActivity

<a id="opIdGetLearningActivity"></a>

> Code samples

```http
GET http://localhost:5075/learningactivity HTTP/1.1
Host: localhost:5075
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    /// Make a dummy request
    public async Task MakeGetRequest()
    {
      string url = "http://localhost:5075/learningactivity";
      var result = await GetAsync(url);
    }

    /// Performs a GET Request
    public async Task GetAsync(string url)
    {
        //Start the request
        HttpResponseMessage response = await Client.GetAsync(url);

        //Validate result
        response.EnsureSuccessStatusCode();

    }
    
    
    
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`GET /learningactivity`

<h3 id="getlearningactivity-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|query|integer,string(int32)|false|none|
|name|query|string|false|none|

> Example responses

> 200 Response

```json
{
  "id": 0,
  "name": "string",
  "url": "string",
  "educationLevels": [
    "string"
  ],
  "interestAreaScoreBoosts": [
    {
      "interestArea": "string",
      "skillLevelScoreBoosts": [
        {
          "skillLevel": 0,
          "score": 0
        }
      ]
    }
  ],
  "goalScoreBoosts": [
    {
      "goal": "string",
      "score": 0
    }
  ]
}
```

<h3 id="getlearningactivity-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[GetLearningActivityResponse](#schemagetlearningactivityresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|None|

## CreateLearningActivity

<a id="opIdCreateLearningActivity"></a>

> Code samples

```http
POST http://localhost:5075/learningactivity HTTP/1.1
Host: localhost:5075
Content-Type: application/json
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    
    /// Make a dummy request
    public async Task MakePostRequest()
    {
      string url = "http://localhost:5075/learningactivity";
      
      string json = @"{
  ""name"": ""string"",
  ""url"": ""string"",
  ""educationLevels"": [
    ""string""
  ],
  ""interestAreaScoreBoosts"": [
    {
      ""interestArea"": ""string"",
      ""skillLevelScoreBoosts"": [
        {
          ""skillLevel"": 0,
          ""score"": 0
        }
      ]
    }
  ],
  ""goalScoreBoosts"": [
    {
      ""goal"": ""string"",
      ""score"": 0
    }
  ]
}";
      LearningActivityDto content = JsonConvert.DeserializeObject(json);
      await PostAsync(content, url);
      
      
    }

    /// Performs a POST Request
    public async Task PostAsync(LearningActivityDto content, string url)
    {
        //Serialize Object
        StringContent jsonContent = SerializeObject(content);

        //Execute POST request
        HttpResponseMessage response = await Client.PostAsync(url, jsonContent);
    }
    
    
    
    /// Serialize an object to Json
    private StringContent SerializeObject(LearningActivityDto content)
    {
        //Serialize Object
        string jsonObject = JsonConvert.SerializeObject(content);

        //Create Json UTF8 String Content
        return new StringContent(jsonObject, Encoding.UTF8, "application/json");
    }
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`POST /learningactivity`

> Body parameter

```json
{
  "name": "string",
  "url": "string",
  "educationLevels": [
    "string"
  ],
  "interestAreaScoreBoosts": [
    {
      "interestArea": "string",
      "skillLevelScoreBoosts": [
        {
          "skillLevel": 0,
          "score": 0
        }
      ]
    }
  ],
  "goalScoreBoosts": [
    {
      "goal": "string",
      "score": 0
    }
  ]
}
```

<h3 id="createlearningactivity-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[LearningActivityDto](#schemalearningactivitydto)|true|none|

> Example responses

> 400 Response

```json
{
  "type": null,
  "title": null,
  "status": null,
  "detail": null,
  "instance": null,
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

<h3 id="createlearningactivity-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|204|[No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)|No Content|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[ProblemDetails](#schemaproblemdetails)|



## UpdateLearningActivity

<a id="opIdUpdateLearningActivity"></a>

> Code samples

```http
PUT http://localhost:5075/learningactivity/{id} HTTP/1.1
Host: localhost:5075
Content-Type: application/json
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    
    
    /// Make a dummy request
    public async Task MakePutRequest()
    {
      int id = 1;
      string url = "http://localhost:5075/learningactivity/{id}";

      
      string json = @"{
  ""name"": ""string"",
  ""url"": ""string"",
  ""educationLevels"": [
    ""string""
  ],
  ""interestAreaScoreBoosts"": [
    {
      ""interestArea"": ""string"",
      ""skillLevelScoreBoosts"": [
        {
          ""skillLevel"": 0,
          ""score"": 0
        }
      ]
    }
  ],
  ""goalScoreBoosts"": [
    {
      ""goal"": ""string"",
      ""score"": 0
    }
  ]
}";
      LearningActivityDto content = JsonConvert.DeserializeObject(json);
      var result = await PutAsync(id, content, url);
      
          
    }

    /// Performs a PUT Request
    public async Task PutAsync(int id, LearningActivityDto content, string url)
    {
        //Serialize Object
        StringContent jsonContent = SerializeObject(content);

        //Execute PUT request
        HttpResponseMessage response = await Client.PutAsync(url + $"/{id}", jsonContent);

        //Return response
        return await DeserializeObject(response);
    }
    
    
    /// Serialize an object to Json
    private StringContent SerializeObject(LearningActivityDto content)
    {
        //Serialize Object
        string jsonObject = JsonConvert.SerializeObject(content);

        //Create Json UTF8 String Content
        return new StringContent(jsonObject, Encoding.UTF8, "application/json");
    }
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`PUT /learningactivity/{id}`

> Body parameter

```json
{
  "name": "string",
  "url": "string",
  "educationLevels": [
    "string"
  ],
  "interestAreaScoreBoosts": [
    {
      "interestArea": "string",
      "skillLevelScoreBoosts": [
        {
          "skillLevel": 0,
          "score": 0
        }
      ]
    }
  ],
  "goalScoreBoosts": [
    {
      "goal": "string",
      "score": 0
    }
  ]
}
```

<h3 id="updatelearningactivity-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|
|body|body|[LearningActivityDto](#schemalearningactivitydto)|true|none|

> Example responses

> 400 Response

```json
{
  "type": null,
  "title": null,
  "status": null,
  "detail": null,
  "instance": null,
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}
```

<h3 id="updatelearningactivity-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|204|[No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)|No Content|None|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|None|



## DeleteLearningActivity

<a id="opIdDeleteLearningActivity"></a>

> Code samples

```http
DELETE http://localhost:5075/learningactivity/{id} HTTP/1.1
Host: localhost:5075
Accept: application/problem+json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    
    
    
    /// Make a dummy request
    public async Task MakeDeleteRequest()
    {
      int id = 1;
      string url = "http://localhost:5075/learningactivity/{id}";

      await DeleteAsync(id, url);
    }

    /// Performs a DELETE Request
    public async Task DeleteAsync(int id, string url)
    {
        //Execute DELETE request
        HttpResponseMessage response = await Client.DeleteAsync(url + $"/{id}");

        //Return response
        await DeserializeObject(response);
    }
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`DELETE /learningactivity/{id}`

<h3 id="deletelearningactivity-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)|true|none|

> Example responses

> 401 Response

```json
{
  "type": null,
  "title": null,
  "status": null,
  "detail": null,
  "instance": null
}
```

<h3 id="deletelearningactivity-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|204|[No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)|No Content|None|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|None|

<h1 id="prolearning-api-v1-educationlevelendpoints">EducationLevelEndpoints</h1>

## GetEducationLevels

<a id="opIdGetEducationLevels"></a>

> Code samples

```http
GET http://localhost:5075/educationlevel HTTP/1.1
Host: localhost:5075
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    /// Make a dummy request
    public async Task MakeGetRequest()
    {
      string url = "http://localhost:5075/educationlevel";
      var result = await GetAsync(url);
    }

    /// Performs a GET Request
    public async Task GetAsync(string url)
    {
        //Start the request
        HttpResponseMessage response = await Client.GetAsync(url);

        //Validate result
        response.EnsureSuccessStatusCode();

    }
    
    
    
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`GET /educationlevel`

> Example responses

> 200 Response

```json
[
  {
    "name": "string"
  }
]
```

<h3 id="geteducationlevels-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|

<h3 id="geteducationlevels-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[EducationLevelDto](#schemaeducationleveldto)]|false|none|none|
|» name|string|false|none|none|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="prolearning-api-v1-interestareaendpoints">InterestAreaEndpoints</h1>

## GetInterestAreas

<a id="opIdGetInterestAreas"></a>

> Code samples

```http
GET http://localhost:5075/interestarea HTTP/1.1
Host: localhost:5075
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    /// Make a dummy request
    public async Task MakeGetRequest()
    {
      string url = "http://localhost:5075/interestarea";
      var result = await GetAsync(url);
    }

    /// Performs a GET Request
    public async Task GetAsync(string url)
    {
        //Start the request
        HttpResponseMessage response = await Client.GetAsync(url);

        //Validate result
        response.EnsureSuccessStatusCode();

    }
    
    
    
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`GET /interestarea`

> Example responses

> 200 Response

```json
[
  {
    "name": "string"
  }
]
```

<h3 id="getinterestareas-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|

<h3 id="getinterestareas-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[InterestAreaDto](#schemainterestareadto)]|false|none|none|
|» name|string|false|none|none|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="prolearning-api-v1-goalendpoints">GoalEndpoints</h1>

## GetGoals

<a id="opIdGetGoals"></a>

> Code samples

```http
GET http://localhost:5075/goal HTTP/1.1
Host: localhost:5075
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    /// Make a dummy request
    public async Task MakeGetRequest()
    {
      string url = "http://localhost:5075/goal";
      var result = await GetAsync(url);
    }

    /// Performs a GET Request
    public async Task GetAsync(string url)
    {
        //Start the request
        HttpResponseMessage response = await Client.GetAsync(url);

        //Validate result
        response.EnsureSuccessStatusCode();

    }
    
    
    
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`GET /goal`

> Example responses

> 200 Response

```json
[
  {
    "name": "string"
  }
]
```

<h3 id="getgoals-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|

<h3 id="getgoals-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[GoalDto](#schemagoaldto)]|false|none|none|
|» name|string|false|none|none|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="prolearning-api-v1-recommendationsendpoints">RecommendationsEndpoints</h1>

## get__recommendations

> Code samples

```http
GET http://localhost:5075/recommendations?interestAreas=string&skillLevels=0&goals=string HTTP/1.1
Host: localhost:5075
Accept: application/json

```

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <<summary>>
/// Example of Http Client
/// <</summary>>
public class HttpExample
{
    private HttpClient Client { get; set; }

    /// <<summary>>
    /// Setup http client
    /// <</summary>>
    public HttpExample()
    {
      Client = new HttpClient();
    }
    
    /// Make a dummy request
    public async Task MakeGetRequest()
    {
      string url = "http://localhost:5075/recommendations";
      var result = await GetAsync(url);
    }

    /// Performs a GET Request
    public async Task GetAsync(string url)
    {
        //Start the request
        HttpResponseMessage response = await Client.GetAsync(url);

        //Validate result
        response.EnsureSuccessStatusCode();

    }
    
    
    
    
    /// Deserialize object from request response
    private async Task DeserializeObject(HttpResponseMessage response)
    {
        //Read body 
        string responseBody = await response.Content.ReadAsStringAsync();

        //Deserialize Body to object
        var result = JsonConvert.DeserializeObject(responseBody);
    }
}

```

`GET /recommendations`

<h3 id="get__recommendations-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|interestAreas|query|array[string]|true|none|
|skillLevels|query|array[integer,string]|true|none|
|goals|query|array[string]|true|none|
|educationLevel|query|string|false|none|
|page|query|integer,string(int32)|false|none|
|pageSize|query|integer,string(int32)|false|none|

> Example responses

> 200 Response

```json
{
  "items": [
    {
      "name": "string",
      "url": "string",
      "score": 0,
      "scoreBreakdown": {
        "interestAreas": [
          {
            "interestArea": "string",
            "skillLevel": 0,
            "score": 0
          }
        ],
        "goals": [
          {
            "goal": "string",
            "score": 0
          }
        ]
      }
    }
  ],
  "page": 0,
  "pageSize": 0,
  "totalCount": 0,
  "hasNextPage": true,
  "hasPreviousPage": true
}
```

<h3 id="get__recommendations-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[PagedListOfGetRecommendationsResponse](#schemapagedlistofgetrecommendationsresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[HttpValidationProblemDetails](#schemahttpvalidationproblemdetails)|



# Schemas

<h2 id="tocS_EducationLevelDto">EducationLevelDto</h2>
<!-- backwards compatibility -->
<a id="schemaeducationleveldto"></a>
<a id="schema_EducationLevelDto"></a>
<a id="tocSeducationleveldto"></a>
<a id="tocseducationleveldto"></a>

```json
{
  "name": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|false|none|none|

<h2 id="tocS_GetLearningActivityResponse">GetLearningActivityResponse</h2>
<!-- backwards compatibility -->
<a id="schemagetlearningactivityresponse"></a>
<a id="schema_GetLearningActivityResponse"></a>
<a id="tocSgetlearningactivityresponse"></a>
<a id="tocsgetlearningactivityresponse"></a>

```json
{
  "id": 0,
  "name": "string",
  "url": "string",
  "educationLevels": [
    "string"
  ],
  "interestAreaScoreBoosts": [
    {
      "interestArea": "string",
      "skillLevelScoreBoosts": [
        {
          "skillLevel": 0,
          "score": 0
        }
      ]
    }
  ],
  "goalScoreBoosts": [
    {
      "goal": "string",
      "score": 0
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer,string(int32)|false|none|none|
|name|string|false|none|none|
|url|string|false|none|none|
|educationLevels|[string]|false|none|none|
|interestAreaScoreBoosts|[[InterestAreaScoreBoost](#schemainterestareascoreboost)]|false|none|none|
|goalScoreBoosts|[[GoalScoreBoost](#schemagoalscoreboost)]|false|none|none|

<h2 id="tocS_GetRecommendationsResponse">GetRecommendationsResponse</h2>
<!-- backwards compatibility -->
<a id="schemagetrecommendationsresponse"></a>
<a id="schema_GetRecommendationsResponse"></a>
<a id="tocSgetrecommendationsresponse"></a>
<a id="tocsgetrecommendationsresponse"></a>

```json
{
  "name": "string",
  "url": "string",
  "score": 0,
  "scoreBreakdown": {
    "interestAreas": [
      {
        "interestArea": "string",
        "skillLevel": 0,
        "score": 0
      }
    ],
    "goals": [
      {
        "goal": "string",
        "score": 0
      }
    ]
  }
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|false|none|none|
|url|string|false|none|none|
|score|integer,string(int32)|false|none|none|
|scoreBreakdown|[ScoreBreakdownDto](#schemascorebreakdowndto)|false|none|none|

<h2 id="tocS_GoalDto">GoalDto</h2>
<!-- backwards compatibility -->
<a id="schemagoaldto"></a>
<a id="schema_GoalDto"></a>
<a id="tocSgoaldto"></a>
<a id="tocsgoaldto"></a>

```json
{
  "name": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|false|none|none|

<h2 id="tocS_GoalScoreBoost">GoalScoreBoost</h2>
<!-- backwards compatibility -->
<a id="schemagoalscoreboost"></a>
<a id="schema_GoalScoreBoost"></a>
<a id="tocSgoalscoreboost"></a>
<a id="tocsgoalscoreboost"></a>

```json
{
  "goal": "string",
  "score": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|goal|string|false|none|none|
|score|integer,string(int32)|false|none|none|

<h2 id="tocS_GoalScoreBreakdown">GoalScoreBreakdown</h2>
<!-- backwards compatibility -->
<a id="schemagoalscorebreakdown"></a>
<a id="schema_GoalScoreBreakdown"></a>
<a id="tocSgoalscorebreakdown"></a>
<a id="tocsgoalscorebreakdown"></a>

```json
{
  "goal": "string",
  "score": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|goal|string|false|none|none|
|score|integer,string(int32)|false|none|none|

<h2 id="tocS_HttpValidationProblemDetails">HttpValidationProblemDetails</h2>
<!-- backwards compatibility -->
<a id="schemahttpvalidationproblemdetails"></a>
<a id="schema_HttpValidationProblemDetails"></a>
<a id="tocShttpvalidationproblemdetails"></a>
<a id="tocshttpvalidationproblemdetails"></a>

```json
{
  "type": null,
  "title": null,
  "status": null,
  "detail": null,
  "instance": null,
  "errors": {
    "property1": [
      "string"
    ],
    "property2": [
      "string"
    ]
  }
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|type|null,string|false|none|none|
|title|null,string|false|none|none|
|status|null,integer,string(int32)|false|none|none|
|detail|null,string|false|none|none|
|instance|null,string|false|none|none|
|errors|object|false|none|none|
|» **additionalProperties**|[string]|false|none|none|

<h2 id="tocS_InterestAreaDto">InterestAreaDto</h2>
<!-- backwards compatibility -->
<a id="schemainterestareadto"></a>
<a id="schema_InterestAreaDto"></a>
<a id="tocSinterestareadto"></a>
<a id="tocsinterestareadto"></a>

```json
{
  "name": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|false|none|none|

<h2 id="tocS_InterestAreaScoreBoost">InterestAreaScoreBoost</h2>
<!-- backwards compatibility -->
<a id="schemainterestareascoreboost"></a>
<a id="schema_InterestAreaScoreBoost"></a>
<a id="tocSinterestareascoreboost"></a>
<a id="tocsinterestareascoreboost"></a>

```json
{
  "interestArea": "string",
  "skillLevelScoreBoosts": [
    {
      "skillLevel": 0,
      "score": 0
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|interestArea|string|false|none|none|
|skillLevelScoreBoosts|[[SkillLevelScoreBoost](#schemaskilllevelscoreboost)]|false|none|none|

<h2 id="tocS_InterestAreaScoreBreakdown">InterestAreaScoreBreakdown</h2>
<!-- backwards compatibility -->
<a id="schemainterestareascorebreakdown"></a>
<a id="schema_InterestAreaScoreBreakdown"></a>
<a id="tocSinterestareascorebreakdown"></a>
<a id="tocsinterestareascorebreakdown"></a>

```json
{
  "interestArea": "string",
  "skillLevel": 0,
  "score": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|interestArea|string|false|none|none|
|skillLevel|[SkillLevel](#schemaskilllevel)|false|none|none|
|score|integer,string(int32)|false|none|none|

<h2 id="tocS_LearningActivityDto">LearningActivityDto</h2>
<!-- backwards compatibility -->
<a id="schemalearningactivitydto"></a>
<a id="schema_LearningActivityDto"></a>
<a id="tocSlearningactivitydto"></a>
<a id="tocslearningactivitydto"></a>

```json
{
  "name": "string",
  "url": "string",
  "educationLevels": [
    "string"
  ],
  "interestAreaScoreBoosts": [
    {
      "interestArea": "string",
      "skillLevelScoreBoosts": [
        {
          "skillLevel": 0,
          "score": 0
        }
      ]
    }
  ],
  "goalScoreBoosts": [
    {
      "goal": "string",
      "score": 0
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|false|none|none|
|url|string|false|none|none|
|educationLevels|[string]|false|none|none|
|interestAreaScoreBoosts|[[InterestAreaScoreBoost](#schemainterestareascoreboost)]|false|none|none|
|goalScoreBoosts|[[GoalScoreBoost](#schemagoalscoreboost)]|false|none|none|

<h2 id="tocS_PagedListOfGetRecommendationsResponse">PagedListOfGetRecommendationsResponse</h2>
<!-- backwards compatibility -->
<a id="schemapagedlistofgetrecommendationsresponse"></a>
<a id="schema_PagedListOfGetRecommendationsResponse"></a>
<a id="tocSpagedlistofgetrecommendationsresponse"></a>
<a id="tocspagedlistofgetrecommendationsresponse"></a>

```json
{
  "items": [
    {
      "name": "string",
      "url": "string",
      "score": 0,
      "scoreBreakdown": {
        "interestAreas": [
          {
            "interestArea": "string",
            "skillLevel": 0,
            "score": 0
          }
        ],
        "goals": [
          {
            "goal": "string",
            "score": 0
          }
        ]
      }
    }
  ],
  "page": 0,
  "pageSize": 0,
  "totalCount": 0,
  "hasNextPage": true,
  "hasPreviousPage": true
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|items|[[GetRecommendationsResponse](#schemagetrecommendationsresponse)]|false|none|none|
|page|integer,string(int32)|false|none|none|
|pageSize|integer,string(int32)|false|none|none|
|totalCount|integer,string(int32)|false|none|none|
|hasNextPage|boolean|false|none|none|
|hasPreviousPage|boolean|false|none|none|

<h2 id="tocS_ProblemDetails">ProblemDetails</h2>
<!-- backwards compatibility -->
<a id="schemaproblemdetails"></a>
<a id="schema_ProblemDetails"></a>
<a id="tocSproblemdetails"></a>
<a id="tocsproblemdetails"></a>

```json
{
  "type": null,
  "title": null,
  "status": null,
  "detail": null,
  "instance": null
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|type|null,string|false|none|none|
|title|null,string|false|none|none|
|status|null,integer,string(int32)|false|none|none|
|detail|null,string|false|none|none|
|instance|null,string|false|none|none|

<h2 id="tocS_ScoreBreakdownDto">ScoreBreakdownDto</h2>
<!-- backwards compatibility -->
<a id="schemascorebreakdowndto"></a>
<a id="schema_ScoreBreakdownDto"></a>
<a id="tocSscorebreakdowndto"></a>
<a id="tocsscorebreakdowndto"></a>

```json
{
  "interestAreas": [
    {
      "interestArea": "string",
      "skillLevel": 0,
      "score": 0
    }
  ],
  "goals": [
    {
      "goal": "string",
      "score": 0
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|interestAreas|[[InterestAreaScoreBreakdown](#schemainterestareascorebreakdown)]|false|none|none|
|goals|[[GoalScoreBreakdown](#schemagoalscorebreakdown)]|false|none|none|

<h2 id="tocS_SkillLevel">SkillLevel</h2>
<!-- backwards compatibility -->
<a id="schemaskilllevel"></a>
<a id="schema_SkillLevel"></a>
<a id="tocSskilllevel"></a>
<a id="tocsskilllevel"></a>

```json
0

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|integer|false|none|none|

<h2 id="tocS_SkillLevelScoreBoost">SkillLevelScoreBoost</h2>
<!-- backwards compatibility -->
<a id="schemaskilllevelscoreboost"></a>
<a id="schema_SkillLevelScoreBoost"></a>
<a id="tocSskilllevelscoreboost"></a>
<a id="tocsskilllevelscoreboost"></a>

```json
{
  "skillLevel": 0,
  "score": 0
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|skillLevel|[SkillLevel](#schemaskilllevel)|false|none|none|
|score|integer,string(int32)|false|none|none|

