import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import App from './App';
import { TopPerformance } from './models';
import * as service from './services'; // Import the entire module

jest.mock('./services', () => ({
  getBranches: jest.fn(),
  getTopPerformancesByBranch: jest.fn()
}));

describe('App', () => {
  const mockData: TopPerformance[] = [
    { month: 1, sellerName: 'Seller A', orderCount: 1, totalPrice: 300 },
    { month: 2, sellerName: 'Seller B', orderCount: 2, totalPrice: 200 },
  ];

  beforeEach(() => {
    (service.getBranches as jest.Mock).mockResolvedValue(['Branch 1', 'Branch 2']);
    (service.getTopPerformancesByBranch as jest.Mock).mockResolvedValue(mockData);
  });

  it('renders without crashing', async () => {
    render(<App />);
    await waitFor(() => {
      // Ensure the select has loaded (branches fetched)
      expect(screen.getByText(/select a branch/i)).toBeInTheDocument();
    });
    // Check if the title is rendered
    expect(screen.getByText('Monthly Top Sellers')).toBeInTheDocument();
  });

  it('displays empty table when no data is available', async () => {
    render(<App />);
    await waitFor(() => {
      // Ensure the select has loaded (branches fetched)
      expect(screen.getByText(/select a branch/i)).toBeInTheDocument();
    });
    // Check that initially, with no branch selected, the table header is there
    expect(screen.queryByText('Month')).toBeInTheDocument();
    expect(screen.queryByText('Seller Name')).toBeInTheDocument();
    expect(screen.queryByText('Count of Total Orders')).toBeInTheDocument();
    expect(screen.queryByText('Total Price')).toBeInTheDocument();
  });


  it('fetches and displays data when a branch is selected', async () => {

    render(<App />);

    // Simulate selecting a branch
    fireEvent.change(screen.getByRole('combobox'), { target: { value: 'Branch 1' } });

    // Wait for the data to be displayed in the table
    await waitFor(() => {
      expect(screen.getByText('January')).toBeInTheDocument();
      expect(screen.getByText('Seller A')).toBeInTheDocument();
      expect(screen.getByText('$300.00')).toBeInTheDocument();
    });
  });
});
