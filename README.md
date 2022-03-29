# MeFit

MeFit is an application built for managing workout goals. Regular users of the application can view programs, workouts and exercises, as well as update their user profiles. They can then set a goal for themselves based on a program, and optionally add extra workouts to their goal. Contributors (and admins) will also have the option to create and edit exisiting workouts, programs and exercises.

This repository contains the backend of the application, which is an ASP.NET Web API created in C# .NET 5 framework using Keycloak for user authentication.

For database creation we are using Entity Framework Core. ERD which the database is based on can be found here. ***TODO ADD LINK***

API documentation can be found later in this Readme. Additionally, when running this project in development mode, Swagger documentation can be viewed.

##### Table of Contents  
[[_TOC_]]

## Deployment
Deployed to Azure with continuous deployment. Requests require a valid token from our deployed Keycloak instance.

[MeFit Azure API](https://mefit.azurewebsites.net/)

## Keycloak setup
In order to run the full application, a local instance of Keycloak is required. After getting your own local instance of [Keycloak](https://hub.docker.com/r/jboss/keycloak/), create a realm for the app. Click on clients -> create. Give the client a name and save. Then update these values for the client:

- Valid redirect URIs: http://localhost:3000/*
- Web Origins: http://localhost:3000

Then enable user registration by clicking Realm settings -> Login, and turn user registration on.

The next step is to set up user roles for the client. Click Clients -> {name of client} -> Roles -> Add role.
Give the first role the name "User" and click save.
Repeat the steps for "Contributor". After saving, set "Composite Roles" to ON. Under Client Roles, select {name of client}. The newly created "User" role should appear. Select "User" and click Add Selected.
Repeat the same steps for "Admin", but select "Contributor" instead of "User" under Available roles and add it.

Next, set "User" as the default role. Click Roles (in the sidebar) -> Default Roles. Select {name of client} in Client Roles. All three user roles should appear under Available roles. Select "User" and Add Selected.

Lastly, we need these roles to be passed in the tokens. Click Clients -> {name of client} -> Mappers -> Create. Give it the following values:
- Name: user_role
- Mapper Type: User Client Role
- Client ID: {name of client}
- Claim JSON Type: string

Keep the remaining settings to default and save. After saving, verify the Token Claim Name for the mapper is user_role.

## Install

**For this project you will also need to install the [frontend](https://gitlab.com/g5453/frontend) and have a running Keycloak(see Keycloak setup above) instance running on your computer.**

Clone to a local directory:
```bash
git clone https://gitlab.com/g5453/backend.git
```

In appsettings.Development.json, change the following variables:

- "LocalDevelopment": should be your SQL server connection string
- "IssuerURI" & "KeyURI": should be the issuer and key links for your own Keycloak instance.

Open solution in Visual Studio or another IDE. In the package-manager console, run the following command:

```bash
update-database
```

## Usage

Run the project in Visual Studio, and test the APIs in Postman with valid tokens, or test it from the [frontend application](https://gitlab.com/g5453/frontend).

## Maintainers

Stian Økland [@StianOkland](https://gitlab.com/StianOkland)<br />
Isak Hauknes [@larrycaw](https://gitlab.com/larrycaw)<br />
Andrea Hårseth Nakstad [@andreahn](https://gitlab.com/anakstad)

## API documentation

These endpoints allow you to handle communication with MeFit database deployed on azure

### GET /api/Exercises/all
Get all exercise data
<details>
 <summary><b>200 Success</b> - Sample response</summary>

```
[
  {
    "id": 0,
    "name": "string",
    "description": "string",
    "targetMuscleGroup": "string",
    "imageURL": "string",
    "videoURL": "string"
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
 <summary><b>200 Success</b> - Sample response</summary>

```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "targetMuscleGroup": "string",
  "imageURL": "string",
  "videoURL": "string"
}
```
</details>

### GET /api/Goals/all
Get all goals with associated program and workouts
<details>
 <summary><b>200 Success</b> - Sample response</summary>

```
[
  {
    "id": 0,
    "programEndDate": "2022-03-29T19:29:46.680Z",
    "achieved": true,
    "programId": 0,
    "workoutGoals": [
      {
        "workoutId": 0,
        "goalId": 0,
        "completed": true
      }
    ],
    "profileId": "string"
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
 <summary><b>200 Success</b> - Sample response</summary>

```
{
  "id": 0,
  "programEndDate": "2022-03-28T08:04:41.396Z",
  "achieved": true,
  "programId": 0,
  "workoutGoals": [
    {
      "workoutId": 0,
      "goalId": 0,
      "completed": true
    }
  ],
  "profileId": "string"
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
 <summary><b>200 Success</b> - Sample response</summary>

```
[
  {
    "programEndDate": "2022-03-28T08:05:01.890Z",
    "achieved": true,
    "programId": 0,
    "workoutGoals": [
      {
        "workoutId": 0,
        "goalId": 0,
        "completed": true
      }
    ]
  }
]
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
  "programEndDate": "2022-03-28T08:05:35.902Z",
  "achieved": true,
  "programId": 0,
  "workoutGoals": [
    {
      "workoutId": 0,
      "goalId": 0,
      "completed": true
    }
  ]
}
```
</details>

### GET /api/GoalWorkout/all
Get all workouts associated with goals -- here todo - update response

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

### POST /api/Exercise
Adds a new exercise to the database

**Parameters**

No parameters

**Sample request body**

```
{
  "name": "string",
  "description": "string",
  "targetMuscleGroup": "string",
  "imageURL": "string",
  "videoURL": "string"
}
```

<details>
 <summary><b>201 Created</b> - sample response</summary>

Exercise was successfully created 

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

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>
 
 Could happen if the request body has the incorrect format.

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

</details>

### POST /api/Goals
Adds a new goal to the database.

**Parameters**

No parameters

**Sample request body**

```
{
  "programEndDate": "2022-03-29T20:05:03.849Z",
  "programId": 0,
  "profileId": "string"
}
```

<details>
 <summary><b>201 Created</b> - sample response</summary>

Goal was successfully created 

```
{
  "id": 0,
  "programEndDate": "2022-03-29T20:05:03.853Z",
  "achieved": true,
  "programId": 0,
  "workoutGoals": [
    {
      "workoutId": 0,
      "goalId": 0,
      "completed": true
    }
  ],
  "profileId": "string"
}
```
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>
 
 Could happen if the request body has the incorrect format.

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

</details>

### POST /api/Programs
Adds a new program to the database

**Parameters**

No parameters

**Sample request body**

```
{
  "name": "string",
  "category": "string"
}
```

<details>
 <summary><b>201 Created</b> - sample response</summary>

Program was successfully created 

```
{
  "id": 0,
  "name": "string",
  "category": "string",
  "workouts": [
    {
      "id": 0,
      "name": "string",
      "type": "string",
      "sets": [
        {
          "id": 0,
          "exerciseRepetitions": 0,
          "exerciseId": 0,
          "exercise": {
            "id": 0,
            "name": "string",
            "description": "string",
            "targetMuscleGroup": "string",
            "imageURL": "string",
            "videoURL": "string"
          },
          "workouts": [
            null
          ]
        }
      ],
      "programs": [
        null
      ],
      "workoutGoals": [
        {
          "workoutId": 0,
          "goalId": 0,
          "goal": {
            "id": 0,
            "programEndDate": "2022-03-29T20:06:56.150Z",
            "achieved": true,
            "programId": 0,
            "profileId": "string",
            "profile": {
              "id": "string",
              "weight": 0,
              "height": 0,
              "medicalConditions": "string",
              "disabilities": "string"
            },
            "workoutGoals": [
              null
            ]
          },
          "completed": true
        }
      ]
    }
  ]
}
```
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>
 
 Could happen if the request body has the incorrect format.

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

</details>

### POST /api/MFProgram/assignWorkouts
Assigns a workout to a program to the database

**Parameters**

|           Name | Required |  Type  | Description                                                            |
|---------------:|:--------:|:------:|------------------------------------------------------------------------|
|           `id` | required |  int   | Program ID.                                                            |

**Sample request body**
Should contain an array of workout IDs.

```
[
  0
]
```
<details>
 <summary><b>200 Success</b> - sample response</summary>

No content
</details>


### POST /api/Profile
Adds a new profile to the database.

**Parameters**

No parameters

**Sample request body**

```
{
  "id": "string",
  "weight": 0,
  "height": 0,
  "medicalConditions": "string",
  "disabilities": "string"
}
```


<details>
 <summary><b>201 Created</b> - sample response</summary>

Profile was successfully created 

```
{
  "weight": 0,
  "height": 0,
  "medicalConditions": "string",
  "disabilities": "string"
}
```
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>
 
 Could happen if the request body has the incorrect format.

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

</details>

### POST /api/Set
Adds a new set to the database.

**Parameters**

No parameters

**Sample request body**

```
{
  "exerciseRepetitions": 0,
  "exerciseId": 0
}
```

<details>
 <summary><b>201 Created</b> - sample response</summary>

Set was successfully created 

```
{
  "id": 0,
  "exerciseRepetitions": 0,
  "exerciseId": 0,
  "exercise": {
    "id": 0,
    "name": "string",
    "description": "string",
    "targetMuscleGroup": "string",
    "imageURL": "string",
    "videoURL": "string"
  },
  "workouts": [
    {
      "id": 0,
      "name": "string",
      "type": "string",
      "sets": [
        null
      ],
      "programs": [
        {
          "id": 0,
          "name": "string",
          "category": "string",
          "workouts": [
            null
          ]
        }
      ],
      "workoutGoals": [
        {
          "workoutId": 0,
          "goalId": 0,
          "goal": {
            "id": 0,
            "programEndDate": "2022-03-29T20:19:58.383Z",
            "achieved": true,
            "programId": 0,
            "program": {
              "id": 0,
              "name": "string",
              "category": "string",
              "workouts": [
                null
              ]
            },
            "profileId": "string",
            "profile": {
              "id": "string",
              "weight": 0,
              "height": 0,
              "medicalConditions": "string",
              "disabilities": "string"
            },
            "workoutGoals": [
              null
            ]
          },
          "completed": true
        }
      ]
    }
  ]
}
```
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>
 
 Could happen if the request body has the incorrect format.

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

</details>

### POST /api/Workouts
Adds a new workout to the database, and does not assign any sets to it.

**Parameters**

No parameters

**Sample request body**

```
{
  "name": "string",
  "type": "string"
}
```

<details>
 <summary><b>201 Created</b> - sample response</summary>

Workout was successfully created

```
{
  "id": 0,
  "name": "string",
  "type": "string",
  "sets": [
    {
      "id": 0,
      "exerciseRepetitions": 0,
      "exerciseId": 0,
      "exercise": {
        "id": 0,
        "name": "string",
        "description": "string",
        "targetMuscleGroup": "string",
        "imageURL": "string",
        "videoURL": "string"
      },
      "workouts": [
        null
      ]
    }
  ],
  "programs": [
    {
      "id": 0,
      "name": "string",
      "category": "string",
      "workouts": [
        null
      ]
    }
  ],
  "workoutGoals": [
    {
      "workoutId": 0,
      "goalId": 0,
      "goal": {
        "id": 0,
        "programEndDate": "2022-03-29T20:24:53.743Z",
        "achieved": true,
        "programId": 0,
        "program": {
          "id": 0,
          "name": "string",
          "category": "string",
          "workouts": [
            null
          ]
        },
        "profileId": "string",
        "profile": {
          "id": "string",
          "weight": 0,
          "height": 0,
          "medicalConditions": "string",
          "disabilities": "string"
        },
        "workoutGoals": [
          null
        ]
      },
      "completed": true
    }
  ]
}
```
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>
 
 Could happen if the request body has the incorrect format.

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```

</details>

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


### DELETE /api/Exercises/delete
Deletes an exercise from the database.

**Parameters**

|               Name | Required |  Type   | Description                                            |
|-------------------:|:--------:|:-------:|--------------------------------------------------------|
|               `id` | required |   int   | Exercise ID.                                           |


<details>
 <summary><b>200 Success</b> - sample response</summary>

No content
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```
</details>

### DELETE /api/Goals/delete
Deletes a goal from the database

**Parameters**

|               Name | Required |  Type   | Description                                    |
|-------------------:|:--------:|:-------:|------------------------------------------------|
|               `id` | required |   int   | Goal ID. <br/><br/> Supported values: `goalId` |

<details>
 <summary><b>200 Success</b> - sample response</summary>

No content
</details>

<details>
 <summary><b>404 Not Found</b> - sample response</summary>

 Could happen if the goal does not exist in the database

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```
</details>

<details>
 <summary><b>400 Bad Request</b> - sample response</summary>

```
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string"
}
```
</details>

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

