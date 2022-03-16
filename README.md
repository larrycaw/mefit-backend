# MeFit

MeFit is an application built for managing weekly workout goals.
Users of the application can get an exercise scheme based on programs which 
include exercises and sets. These schemes are catered to the users goals.

This repository contains the backend of the application, 
which is an ASP.NET Web API created in C# .NET 5 framework using Keycloak for user authentication.
For database creating we are using Enitity Framework Core for database creating and for API documentation we are using the extension Swashbuckle which allows us to use Swagger (a collection of html, css and js used to autogenerate a documentation UI).

TODO: Write more about what the database contains (tables) and the relationships between them.

# Deployed using Azure with continous deployment

[MeFit Azure API](https://mefit.azurewebsites.net/)

# Install

 - Clone to a local directory
```bash
git clone https://gitlab.com/g5453/backend.git
```
 - Open solution in Visual Studio or another IDE
 - Update to your SQL Server connection info in appsettings.json

# Usage

In the package manager console in Visual Studio:
```bash
update-database
```

Run the project in Visual Studio, and test the APIs in Swagger/Postman.

# Maintainers

Stian Økland [@StianOkland](https://github.com/StianOkland)<br />
Isak Hauknes [@larrycaw](https://github.com/larrycaw)<br />
Andrea Hårseth Nakstad [@andreahn](https://github.com/andreahn)

# API endpoints

These endpoints allow you to handle communication with MeFit database deployed on azure

### GET /api/Addresses/all
Get basic address data for the all users

<details>
 <summary>Sample response</summary>

```
[
  {
    "addressLine1": "string",
    "addressLine2": "string",
    "addressLine3": "string",
    "postalCode": "string",
    "city": "string",
    "country": "string"
  },
  {
    "addressLine1": "string",
    "addressLine2": "string",
    "addressLine3": "string",
    "postalCode": "string",
    "city": "string",
    "country": "string"
  }
]
```

</details>

### GET /api/Addresses
Get basic address data for a single user

**Parameters**

| Name | Required | Type | Description                                           |
|-----:|:--------:|:----:|-------------------------------------------------------|
| `id` | required | int  | Address ID. <br/><br/> Supported values: `addressId`. |

<details>
 <summary>Sample response</summary>

```
[
  {
    "addressLine1": "string",
    "addressLine2": "string",
    "addressLine3": "string",
    "postalCode": "string",
    "city": "string",
    "country": "string"
  }
]
```
</details>

### GET /api/Exercises/all
Get all exercise data
<details>
 <summary>Response</summary>

```
 [
   {
     "name": "Barbell curl",
     "description": "Biceps strong",
     "targetMuscleGroup": "biceps",
     "imageURL": "img",
     "videoURL": ".mov"
   },
   {
     "name": "Leg press",
     "description": "Legs strong",
     "targetMuscleGroup": "legs",
     "imageURL": "img",
     "videoURL": ".mov"
   },
   {
     "name": "Push up",
     "description": "Chest strong",
     "targetMuscleGroup": "upper body",
     "imageURL": "img",
     "videoURL": ".mov"
   },
   {
     "name": "Isolation curl",
     "description": "Biceps strong",
     "targetMuscleGroup": "biceps",
     "imageURL": "img",
     "videoURL": ".mov"
   }
 ]
```
</details>

### GET /api/Exercises
Get basic exercise data

**Parameters**

| Name | Required | Type | Description                                             |
|-----:|:--------:|:----:|---------------------------------------------------------|
| `id` | required | int  | Exercise ID. <br/><br/> Supported values: `exerciseId`. |


<details>
 <summary>Sample response</summary>

```
 {
   "name": "Barbell curl",
   "description": "Biceps strong",
   "targetMuscleGroup": "biceps",
   "imageURL": "img",
   "videoURL": ".mov"
 }
```
</details>

### GET /api/Goals/all
Get all goals with associated program and workouts
<details>
 <summary>Sample response</summary>

```
 [
   {
     "programEndDate": "2022-03-14T13:34:40.1503317",
     "achieved": true,
     "programId": 2,
     "workouts": [
       2
     ],
     "profileId": "keycloak-uid"
   },
   {
     "programEndDate": "2022-03-14T13:34:40.1503372",
     "achieved": true,
     "programId": 2,
     "workouts": [],
     "profileId": "keycloak-uid"
   },
   {
     "programEndDate": "2022-03-14T14:32:00.127",
     "achieved": true,
     "programId": 3,
     "workouts": [
       2,
       3
     ],
     "profileId": "keycloak-uid"
   },
   {
     "programEndDate": "2022-03-14T14:09:35.292",
     "achieved": false,
     "programId": 1,
     "workouts": [],
     "profileId": "keycloak-uid"
   }
 ]
```
</details>

### GET /api/Goals
Get a goal with associated program and workouts

**Parameters**

|          Name | Required | Type | Description                                     |
|--------------:|:--------:|:----:|-------------------------------------------------|
|          `id` | required | int  | Goal ID. <br/><br/> Supported values: `goalId`. |

<details>
 <summary>Sample response</summary>

```
 {
  "programEndDate": "2022-03-14T13:34:40.1503317",
  "achieved": true,
  "programId": 2,
  "workouts": [
    2
  ],
  "profileId": "keycloak-uid"
 }
```
</details>

### GET /api/Goals/user
Get a users goals

**Parameters**

| Name | Required | Type | Description                                     |
|-----:|:--------:|:----:|-------------------------------------------------|
| `id` | required | int  | User ID. <br/><br/> Supported values: `userId`. |


<details>
 <summary>Response</summary>

```
{
  "programEndDate": "2022-03-14T14:09:35.292",
  "achieved": false,
  "programId": 1,
  "workouts": []
}
```
</details>

### GET /api/Goals/currentGoal
Get a users current goal

**Parameters**

| Name | Required | Type | Description                                     |
|-----:|:--------:|:----:|-------------------------------------------------|
| `id` | required | int  | User ID. <br/><br/> Supported values: `userId`. |

<details>
 <summary>Response</summary>

```
{
  "programEndDate": "2022-03-14T14:09:35.292",
  "achieved": false,
  "programId": 1,
  "workouts": []
}
```
</details>

### GET /api/MFProgram/all
Get all programs with associated workouts

<details>
 <summary>Response</summary>

```
[
  {
    "name": "Bicep enhancement",
    "category": "Upper body",
    "workouts": [
      1
    ]
  },
  {
    "name": "Strength building",
    "category": "Whole body",
    "workouts": [
      1,
      2
    ]
  },
  {
    "name": "Cardio",
    "category": "Whole body",
    "workouts": [
      3
    ]
  }
]
```
</details>

### GET /api/MFProgram
Get a program with associated workouts

**Parameters**

| Name | Required | Type | Description                                           |
|-----:|:--------:|:----:|-------------------------------------------------------|
| `id` | required | int  | Program ID. <br/><br/> Supported values: `programId`. |

<details>
 <summary>Response</summary>

```
{
  "name": "Bicep enhancement",
  "category": "Upper body",
  "workouts": [
    1
  ]
}
```
</details>

### GET /api/Profile/all
Get all profiles with associated workoutId and setId

<details>
 <summary>Response</summary>

```
[
  {
    "weight": 80,
    "height": 180,
    "workoutId": null,
    "setId": null,
    "medicalConditions": "string",
    "disabilities": "string"
  }
]
```
</details>

### GET /api/Profile
Get a profile with associated workoutId and setId

**Parameters**

| Name | Required |  Type  | Description                                                                                       |
|-----:|:--------:|:------:|---------------------------------------------------------------------------------------------------|
| `id` | required | string | Get a profile by id with workoutId and setId if exists. <br/><br/> Supported values: `profileId`. |

<details>
 <summary>Response</summary>

```
{
  "weight": 80,
  "height": 180,
  "workoutId": null,
  "setId": null,
  "medicalConditions": "string",
  "disabilities": "string"
}
```
</details>

### GET /api/Profile/currentGoal
Get a users current goal with associated workouts

**Parameters**

| Name | Required |  Type  | Description                                     |
|-----:|:--------:|:------:|-------------------------------------------------|
| `id` | required | string | Goal ID. <br/><br/> Supported values: `userId`. |


<details>
 <summary>Response</summary>

```
{
  "programEndDate": "2022-03-14T14:09:35.292",
  "achieved": false,
  "programId": 1,
  "workouts": []
}
```
</details>

### GET /api/Set/all
Get all sets and associated exerciseId and workoutId's

<details>
 <summary>Response</summary>

```
]
  {
    "exerciseRepetitions": 10,
    "exerciseId": 1,
    "workouts": [
      1,
      2
    ]
  },
  {
    "exerciseRepetitions": 20,
    "exerciseId": 2,
    "workouts": [
      1
    ]
  }
]
```
</details>

### GET /api/Set
Get a set with associated exerciseId and workoutId's

**Parameters**

| Name | Required | Type | Description                                   |
|-----:|:--------:|:----:|-----------------------------------------------|
| `id` | required | int  | Set ID. <br/><br/> Supported values: `setId`. |


<details>
 <summary>Response</summary>

```
{
  "exerciseRepetitions": 10,
  "exerciseId": 1,
  "workouts": [
    1,
    2
  ]
}
```
</details>

### GET /api/Workouts/all
Get all workouts with associated goals, sets and programs

<details>
 <summary>Response</summary>

```
[
  {
    "name": "Arm day",
    "type": "Strength",
    "complete": false,
    "sets": [
      1,
      2
    ],
    "goals": [],
    "programs": [
      1,
      2
    ]
  },
  {
    "name": "Leg day",
    "type": "Strength",
    "complete": true,
    "sets": [
      1
    ],
    "goals": [
      2,
      4
    ],
    "programs": [
      2
    ]
  },
  {
    "name": "Running",
    "type": "Cardio",
    "complete": false,
    "sets": [],
    "goals": [
      4
    ],
    "programs": [
      3
    ]
  }
]
```
</details>

### GET /api/Workouts
Get a workout with associated goals, sets and programs


**Parameters**

| Name | Required | Type | Description                                           |
|-----:|:--------:|:----:|-------------------------------------------------------|
| `id` | required | int  | Workout ID. <br/><br/> Supported values: `workoutId`. |


<details>
 <summary>Response</summary>

```
{
  "name": "Arm day",
  "type": "Strength",
  "complete": false,
  "sets": [
    1,
    2
  ],
  "goals": [],
  "programs": [
    1,
    2
  ]
}
```
</details>

___

### POST /api/Addresses
Adds a new user to the database

**Parameters**

|      Name | Required |  Type   | Description                                                |
|----------:|:--------:|:-------:|------------------------------------------------------------|
| `address` | required | Address | User object to add. <br/><br/> Supported values: `address` |

<details>
 <summary>Response</summary>

```
{
  "addressLine1": "Johns Road 2B",
  "addressLine2": null,
  "addressLine3": null,
  "postalCode": "7018",
  "city": "Trondheim",
  "country": null
}
```
</details>

### POST /api/Exercise
Adds a new exercise to the database

**Parameters**

|       Name | Required |   Type   | Description                                                     |
|-----------:|:--------:|:--------:|-----------------------------------------------------------------|
| `exercise` | required | Exercise | Exercise object to add. <br/><br/> Supported values: `exercise` |

<details>
 <summary>Response</summary>

```
{
  "name": "Bench press",
  "description": "Workout on bench with a pole bearing weights",
  "targetMuscleGroup": "Chest",
  "imageURL": null,
  "videoURL": null
}
```
</details>

### POST /api/Goals
Adds a new goal to the database

**Parameters**

|   Name | Required | Type | Description                                             |
|-------:|:--------:|:----:|---------------------------------------------------------|
| `goal` | required | Goal | Goal object to add. <br/><br/> Supported values: `goal` |

<details>
 <summary>Response</summary>

```
{
  "programEndDate": "2022-03-15T19:54:47.094Z",
  "achieved": true,
  "programId": 0,
  "workouts": [
    0
  ],
  "profileId": "string"
}
```
</details>

### POST /api/Programs
Adds a new program to the database

**Parameters**

|      Name | Required |   Type    | Description                                                   |
|----------:|:--------:|:---------:|---------------------------------------------------------------|
| `program` | required | MFProgram | Program object to add. <br/><br/> Supported values: `program` |

<details>
 <summary>Response</summary>

```
{
  "name": "12-week strength building",
  "category": "Strength"
}
```
</details>

### POST /api/MFProgram/assignWorkouts
Assigns a workout to a program to the database

<details>
 <summary>Response</summary>

|           Name | Required |  Type  | Description                                                            |
|---------------:|:--------:|:------:|------------------------------------------------------------------------|
|           `id` | required |  int   | Program ID. <br/><br/> Supported values: `programId`                   |
|   `workoutIds` | required | int[]  | List of workout id's to add. <br/><br/> Supported values: `workoutIds` |
</details>

Gives no response

### POST /api/Profile
Adds a new profile to the database

**Parameters**

|      Name | Required |  Type   | Description                                                   |
|----------:|:--------:|:-------:|---------------------------------------------------------------|
| `profile` | required | Profile | Profile object to add. <br/><br/> Supported values: `profile` |

<details>
 <summary>Response</summary>

```
{
  "weight": 0,
  "height": 0,
  "workoutId": 0,
  "setId": 0,
  "medicalConditions": "string",
  "disabilities": "string"
}
```
</details>

### POST /api/Set
Adds a new set to the database

**Parameters**

|  Name | Required | Type | Description                                           |
|------:|:--------:|:----:|-------------------------------------------------------|
| `set` | required | Set  | Set object to add. <br/><br/> Supported values: `set` |

<details>
 <summary>Response</summary>

```
{
  "exerciseRepetitions": 12,
  "exerciseId": 1
}
```
</details>

### POST /api/Workouts
Adds a new workout to the database

**Parameters**

|      Name | Required |  Type   | Description                                                   |
|----------:|:--------:|:-------:|---------------------------------------------------------------|
| `workout` | required | Workout | Workout object to add. <br/><br/> Supported values: `workout` |

<details>
 <summary>Response</summary>

```
{
  "exerciseRepetitions": 12,
  "exerciseId": 1
}
```
</details>

### POST /api/Workouts/assignSets
Assigns one or more sets to a workout to the database

**Parameters**

|     Name | Required |  Type  | Description                                             |
|---------:|:--------:|:------:|---------------------------------------------------------|
|     `id` | required |  int   | Workout ID. <br/><br/> Supported values: `setId`        |
| `setIds` | required | int[]  | List of set Id's. <br/><br/> Supported values: `setIds` |

Gives no response
___

### PUT /api/Exercises
Edits an exercise in the database

**Parameters**

|       Name | Required |   Type   | Description                                                         |
|-----------:|:--------:|:--------:|---------------------------------------------------------------------|
|       `id` | required |   int    | Exercise ID. <br/><br/> Supported values: `exerciseId`              |
| `exercise` | required | Exercise | Exercise object to replace. <br/><br/> Supported values: `exercise` |

Gives no response

### PUT /api/Addresses
Edits an address in the database

**Parameters**

|      Name | Required |  Type   | Description                                                       |
|----------:|:--------:|:-------:|-------------------------------------------------------------------|
|      `id` | required |   int   | Address ID. <br/><br/> Supported values: `addressId`              |
| `address` | required | Address | Address object to replace. <br/><br/> Supported values: `address` |

Gives no response

### PUT /api/Goals/assignWorkouts
Assigns one or more workouts to a goal

### PUT /api/Goals/assignProgram
Assigns a program to a goal

**Parameters**

|         Name | Required | Type | Description                                          |
|-------------:|:--------:|:----:|------------------------------------------------------|
|     `GoalID` | required | int  | Goal ID. <br/><br/> Supported values: `goalId`       |
| `Program ID` | required | int  | Program ID .<br/><br/> Supported values: `programId` |

Gives no response

### PUT /api/Goals/updateGoal
Updates a goal to the database

**Parameters**

|     Name | Required | Type | Description                                                 |
|---------:|:--------:|:----:|-------------------------------------------------------------|
| `goalID` | required | int  | Goal ID. <br/><br/> Supported values: `goalId`              |
|   `goal` | required | Goal | Goal object to replace. <br/><br/> Supported values: `goal` |

Gives no response

### PUT /api/MFProgram/updateProgram
Edits a program in the database

**Parameters**

|            Name | Required |  Type   | Description                                                       |
|----------------:|:--------:|:-------:|-------------------------------------------------------------------|
|            `id` | required |   int   | Program ID. <br/><br/> Supported values: `programId`              |
| `New goal info` | required | Program | Program object to replace. <br/><br/> Supported values: `program` |

Gives no response

### PUT /api/Profile
Edits a profile in the database

**Parameters**

|             Name | Required |  Type   | Description                                                       |
|-----------------:|:--------:|:-------:|-------------------------------------------------------------------|
|             `id` | required |   int   | Profile ID. <br/><br/> Supported values: `profileId`              |
| `Profile object` | required | Profile | Profile object to replace. <br/><br/> Supported values: `profile` |

Gives no response

### PUT /api/Set/updateSet
Edits a set in the database

**Parameters**

|           Name | Required | Type | Description                                               |
|---------------:|:--------:|:----:|-----------------------------------------------------------|
|           `id` | required | int  | Set ID. <br/><br/> Supported values: `setId`              |
| `New set info` | required | Set  | Set object to replace. <br/><br/> Supported values: `set` |

Gives no response

### PUT /api/Workouts
Edits a workout in the database

**Parameters**

|                          Name | Required |  Type   | Description                                                       |
|------------------------------:|:--------:|:-------:|-------------------------------------------------------------------|
|                          `id` | required |   int   | Workout ID. <br/><br/> Supported values: `addressId`              |
| `Info to update workout with` | required | Workout | Workout object to replace. <br/><br/> Supported values: `workout` |

Gives no response

---


### DELETE /api/Addresses/delete
Deletes an address from the database

**Parameters**

|               Name | Required |  Type   | Description                                          |
|-------------------:|:--------:|:-------:|------------------------------------------------------|
|               `id` | required |   int   | Address ID. <br/><br/> Supported values: `addressId` |

Gives no response

### DELETE /api/Exercises/delete
Deletes an exercise from the database

**Parameters**

|               Name | Required |  Type   | Description                                            |
|-------------------:|:--------:|:-------:|--------------------------------------------------------|
|               `id` | required |   int   | Exercise ID. <br/><br/> Supported values: `exerciseId` |

Gives no response

### DELETE /api/Goals/delete
Deletes a goal from the database

**Parameters**

|               Name | Required |  Type   | Description                                    |
|-------------------:|:--------:|:-------:|------------------------------------------------|
|               `id` | required |   int   | Goal ID. <br/><br/> Supported values: `goalId` |

Gives no response

### DELETE /api/MFProgram/delete
Deletes a program from the database

**Parameters**

|               Name | Required |  Type   | Description                                          |
|-------------------:|:--------:|:-------:|------------------------------------------------------|
|               `id` | required |   int   | Program ID. <br/><br/> Supported values: `programId` |

Gives no response

### DELETE /api/Profile/delete
Deletes a profile from the database

**Parameters**

|               Name | Required |  Type   | Description                                          |
|-------------------:|:--------:|:-------:|------------------------------------------------------|
|               `id` | required |   int   | Profile ID. <br/><br/> Supported values: `profileId` |

Gives no response

