module.exports = {
  preset: 'ts-jest/presets/default-esm', // Use ESM preset
  testEnvironment: 'jsdom',
  transform: {
    '^.+\\.(t|j)sx?$': ['ts-jest', { useESM: true }],
  },
  globals: {
    'ts-jest': {
      useESM: true, // Allow ts-jest to use ESM
    },
  },
  extensionsToTreatAsEsm: ['.ts', '.tsx', '.js'], // Ensure it treats JS/TS files as ESM
  setupFilesAfterEnv: ['<rootDir>/src/setupTests.ts'], // Setup file for jest-dom
  moduleNameMapper: {
    '^@/(.*)$': '<rootDir>/src/$1',  // This is useful for absolute imports
  }
};