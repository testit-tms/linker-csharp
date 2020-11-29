# Test IT - Linker

Linker is a program for transferring tests from IDE (or git repository) to Test IT.
This is a simple example of integration. This should be done automatically in the final version.

To begin:
1. Open Program.cs and set the variables:
Domain - The domain for your test IT instances,
PrivateToken - the user's token. You can take it from your profile Test IT,
ProjectId - Guid, you can take it from yours from swagger (GET /api/v2/projects).
2. Provide metadata from your autotests.
```C#
    await client.CreateAndAddAutoTestToTestCase( {testCaseOrCheckListGlobalId}, {externalId} );
```

Acceptable ratios of autotests and test cases:
* one-to-one
* one-to-many
* many-to-many
