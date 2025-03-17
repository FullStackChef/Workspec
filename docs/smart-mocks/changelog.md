# Changelog

All notable changes to the SmartMocks framework will be documented in this file.

## [Unreleased]

- Placeholder for upcoming changes.

## [0.2.0] - 2025-01-15

### Added

- **MockBuilder**: Introduced the `MockBuilder` class for managing mocks.

  - Support for mock creation, deferred setups, and validation using Moq.
  - Logging for mock creation, retrieval, setup registration, and validation.
  - Integration with `ScenarioBuilder` for mock-driven scenarios.

- **Documentation**: Comprehensive README files for `MockBuilder` and `ScenarioBuilder`.

  - Updated `MockBuilder` README with advanced usage and FAQs.
  - Updated `ScenarioBuilder` README to include advanced usage and integration with `MockBuilder`.

### Updated

- **ScenarioBuilder**:
  - Enhanced API for chaining with mock integration.
  - Improved logging for step addition and scenario creation.

---

## [0.1.0] - 2025-01-14

### Added

- **ScenarioBuilder**: Introduced the `ScenarioBuilder` class, providing a fluent API for defining scenarios in Gherkin syntax.

  - Support for `Given`, `When`, `Then` steps, with `And` steps for additional context.
  - `ToGherkin()` method to output scenarios in valid Gherkin format for documentation and BDD purposes.
  - Logging for scenario creation and step additions using `Microsoft.Extensions.Logging`.

- **Unit Tests**: Comprehensive test suite for `ScenarioBuilder`.

  - Tests for fluent API chaining (e.g., `Given -> When -> Then`).
  - Validation of Gherkin syntax output.
  - Logging verification for scenario creation and step additions.

### Documentation

- Added detailed README for `ScenarioBuilder`, including usage examples and API references.

---

## Future Releases

### Planned Features

- **Workflow Engine**: A feature to execute and validate scenarios programmatically.
- **Integration with Moq**: Build and manage mock APIs seamlessly.
- **Export Options**: Generate Markdown or JSON representations of scenarios for sharing and collaboration.

