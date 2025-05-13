import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import BranchSelector from './BranchSelector';
import { getBranches } from '../../services';

// Mocking getBranches
jest.mock('../../services', () => ({
  getBranches: jest.fn(),
}));


describe('BranchSelector', () => {
  it('renders select dropdown with default option', async () => {
    (getBranches as jest.Mock).mockResolvedValue(['Branch 1', 'Branch 2']);

    render(<BranchSelector onBranchSelect={jest.fn()} />);

    expect(screen.getByText(/select a branch/i)).toBeInTheDocument();

    // Wait for async branches to load
    await waitFor(() => {
      expect(screen.getByText('Branch 1')).toBeInTheDocument();
      expect(screen.getByText('Branch 2')).toBeInTheDocument();
    });
  });

  it('calls onBranchSelect when a branch is selected', async () => {
     (getBranches as jest.Mock).mockResolvedValue(['Branch 1', 'Branch 2']);
    const handleSelect = jest.fn();

    render(<BranchSelector onBranchSelect={handleSelect} />);

    await waitFor(() => screen.getByText('Branch 1'));

    fireEvent.change(screen.getByRole('combobox'), { target: { value: 'Branch 2' } });

    expect(handleSelect).toHaveBeenCalledWith('Branch 2');
  });
});
