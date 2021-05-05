# BDDExampleAPI
This repository uses the example of a qualifications repository and is a demonstration of how BDD can be used to drive integration tests. This approach was something I used on a previous project and it proved invaluable in terms of being able to communicate test scenarios with stakeholders.

## BDDExampleAPI
This is the microservice itself. It contains a limited number of endpoints to carry out basic create, query and delete qualifications.

## BDDExampleAPI.Tests
This contains the integration tests. It uses a couple of nuget packages that are useful for the demonstration:
1. Xunit.Gherkin.Quick
1. FluentAssertions

The basic premise is that a .feature file is created that contains the behaviour required. There is a corresponding file in the Automation directory that provides the glue that essentially translates the BDD Gherkin statements into requests to the API, and assertions performed upon the the result/content returned by API.

## Gotchas
### Cannot find a matching step
The text provided in the .feature is matched to attributes in the C# code ([Given], [When], [And], [Then]). This is done using regular expressions. When initially writing the tests you may encounter exceptions being thrown when running tests. If the detail of the exception indicates that it cannot find a matching step (or similar wording), check the regular expression to ensure it is correct.

### Tests cannot be found
In my experience this has occured for one of two reasons:
The .feature file needs to be copied to the build output directory as part of the build process, so check this is the case. If it is, check the path to the feature file specified in the [FeatureFile] attribute on the C# class to ensure it is correct.
The dotnet class needs to inherit from the Feature class. If not, the tests will not be found / run.
