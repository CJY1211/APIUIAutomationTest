Feature: CompareStars
	

Scenario: Verify the starts value is correct
    When I open GitHub page
    And I search the given username
    And I find any repository belong to that user
    Then I verify that the star value is correct by comparing with result returned from API
    