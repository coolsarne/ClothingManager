# Project .NET Framework

* Naam: Arne Cools
* Academiejaar: 21-22
* Klasgroep: INF203B
* Onderwerp: Clothing Manager

## Sprint 3

### Beide zoekcriteria ingevuld
```
SELECT "d"."Id", "d"."Age", "d"."Name", "d"."Nationality"
FROM "Designers" AS "d"
WHERE ((@__ToLower_0 = '') OR (instr(lower("d"."Name"), @__ToLower_0) > 0)) AND ("d"."Age" = @__age_1)
```

### Enkel zoeken op naam
```
SELECT "d"."Id", "d"."Age", "d"."Name", "d"."Nationality"
FROM "Designers" AS "d"
WHERE (@__ToLower_0 = '') OR (instr(lower("d"."Name"), @__ToLower_0) > 0)
```

### Enkel zoeken op leeftijd
```
SELECT "d"."Id", "d"."Age", "d"."Name", "d"."Nationality"
FROM "Designers" AS "d"
WHERE "d"."Age" = @__age_0
```

### Beide zoekcriteria leeg
```
SELECT "d"."Id", "d"."Age", "d"."Name", "d"."Nationality"
FROM "Designers" AS "d"
```

## Sprint 6

### Nieuwe store

#### Request
```
POST https://localhost:5001/api/stores HTTP/1.1
Accept: application/json
Content-Type: application/json

{
  "city":"Brussels",
  "zipcode":1000,
  "name":"Super Fashion"
}
```
#### Response
```
HTTP/1.1 201 Created
Date: Sun, 02 Jan 2022 17:47:52 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Location: https://localhost:5001/api/stores/6

{
  "city": "Brussels",
  "zipcode": 1000,
  "name": "Super Fashion",
  "clothingPieces": null,
  "id": 6
}

Response code: 201 (Created); Time: 397ms; Content length: 86 bytes
```

### Aanpassen store (success)

#### Request
```
PUT https://localhost:5001/api/stores HTTP/1.1
Accept: application/json
Content-Type: application/json

{
"city":"Paris",
"zipcode":75000,
"name":"Lorette & Linda",
"id": 1
}
```

#### Response
```
HTTP/1.1 200 OK
Date: Sun, 02 Jan 2022 21:17:37 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
"city": "Paris",
"zipcode": 75000,
"name": "Lorette & Linda",
"clothingPieces": null,
"id": 1
}

Response code: 200 (OK); Time: 208ms; Content length: 86 bytes
```

### Aanpassen store (fail)

#### Request
```
PUT https://localhost:5001/api/stores HTTP/1.1
Accept: application/json
Content-Type: application/json

{
"city":"",
"zipcode":75000,
"name":"Lorette & Linda",
"id": 1
}
```

#### Response
```
HTTP/1.1 400 Bad Request
Date: Sun, 02 Jan 2022 21:22:21 GMT
Content-Type: application/problem+json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
"type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
"title": "One or more validation errors occurred.",
"status": 400,
"traceId": "00-f894e1ca3182604583f019a42e0035aa-a56ecff75e40b740-00",
"errors": {
"City": [
"The City field is required."
]
}
}

Response code: 400 (Bad Request); Time: 384ms; Content length: 241 bytes
```
