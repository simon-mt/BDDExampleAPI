Feature: Qualifications API Tests

  Scenario: Single qualification added
    Given there are no saved qualifications
    When the following qualifications are added
        | Qualification Id                     | Course Id                            | Course Name           |
        | 3a495ebc-e151-470b-b571-cbcf9ac60ffc | 0492c7a0-86a8-499c-9548-4e2eb8eb3d0c | Maths                 |
    Then the number of saved qualifications should be 1

  Scenario: Multiple qualifications added
    Given there are no saved qualifications
    When the following qualifications are added
        | Qualification Id                     | Course Id                            | Course Name           |
        | e62316a8-3a25-404d-a7df-77ff4b176317 | f0b675ff-4f56-44c7-85bd-a949add0fed5 | French                |
        | 32fc4cfb-5a72-4f6b-ab40-4383b761a47a | ef5a0821-8cd8-45c8-8413-c1d74ca4fda8 | Geography             |
        | 39d2436e-0f78-4673-ae52-96471c0f65d0 | c39f7993-51aa-4f86-902a-f67bd9d642b1 | Chemistry             |
    Then the number of saved qualifications should be 3

  Scenario: Dont add duplicate qualification
    Given the following qualifications have already been added
        | Qualification Id                     | Course Id                            | Course Name           |
        | e62316a8-3a25-404d-a7df-77ff4b176317 | f0b675ff-4f56-44c7-85bd-a949add0fed5 | French                |
        | 32fc4cfb-5a72-4f6b-ab40-4383b761a47a | ef5a0821-8cd8-45c8-8413-c1d74ca4fda8 | Geography             |
        | 39d2436e-0f78-4673-ae52-96471c0f65d0 | c39f7993-51aa-4f86-902a-f67bd9d642b1 | Chemistry             |
    When the following qualifications are added
        | Qualification Id                     | Course Id                            | Course Name           |
        | 32fc4cfb-5a72-4f6b-ab40-4383b761a47a | ef5a0821-8cd8-45c8-8413-c1d74ca4fda8 | Geography             |
    Then the number of saved qualifications should be 3

  Scenario: Adding duplicate qualification generates an API error status code
    Given the following qualifications have already been added
        | Qualification Id                     | Course Id                            | Course Name           |
        | e62316a8-3a25-404d-a7df-77ff4b176317 | f0b675ff-4f56-44c7-85bd-a949add0fed5 | French                |
        | 32fc4cfb-5a72-4f6b-ab40-4383b761a47a | ef5a0821-8cd8-45c8-8413-c1d74ca4fda8 | Geography             |
        | 39d2436e-0f78-4673-ae52-96471c0f65d0 | c39f7993-51aa-4f86-902a-f67bd9d642b1 | Chemistry             |
    When the following qualifications are added
        | Qualification Id                     | Course Id                            | Course Name           |
        | 32fc4cfb-5a72-4f6b-ab40-4383b761a47a | ef5a0821-8cd8-45c8-8413-c1d74ca4fda8 | Geography             |
    Then the number of HTTP BadRequest status codes should be 1

  Scenario: Read qualification that already exists
    Given the following qualifications have already been added
        | Qualification Id                     | Course Id                            | Course Name           |
        | e62316a8-3a25-404d-a7df-77ff4b176317 | f0b675ff-4f56-44c7-85bd-a949add0fed5 | French                |
        | 32fc4cfb-5a72-4f6b-ab40-4383b761a47a | ef5a0821-8cd8-45c8-8413-c1d74ca4fda8 | Geography             |
        | 39d2436e-0f78-4673-ae52-96471c0f65d0 | c39f7993-51aa-4f86-902a-f67bd9d642b1 | Chemistry             |
    When the qualification with Id 32fc4cfb-5a72-4f6b-ab40-4383b761a47a is requested
    Then the name of the course in the qualification returned should be Geography