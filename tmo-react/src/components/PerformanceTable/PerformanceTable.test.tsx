import { render, screen } from '@testing-library/react';
import PerformanceTable from './PerformanceTable';
import { TopPerformance } from '../../models';

describe('PerformanceTable', () => {
    const sellers: TopPerformance[] = [
        { month: 1, sellerName: 'Seller A', orderCount: 1, totalPrice: 50.000 },
        { month: 2, sellerName: 'Seller B', orderCount: 2, totalPrice: 20.00 },
    ];

      it('renders a table with seller data', () => {
        render(<PerformanceTable sellers={sellers} />);

        // Check if headers are rendered correctly
        expect(screen.getByText('Month')).toBeInTheDocument();
        expect(screen.getByText('Seller Name')).toBeInTheDocument();
        expect(screen.getByText('Count of Total Orders')).toBeInTheDocument();
        expect(screen.getByText('Total Price')).toBeInTheDocument();

        // Check if sellers data is displayed correctly
        expect(screen.getByText('January')).toBeInTheDocument();
        expect(screen.getByText('Seller A')).toBeInTheDocument();
        expect(screen.getByText('1')).toBeInTheDocument();
        expect(screen.getByText('$50.00')).toBeInTheDocument();

        expect(screen.getByText('February')).toBeInTheDocument();
        expect(screen.getByText('Seller B')).toBeInTheDocument();
        expect(screen.getByText('2')).toBeInTheDocument();
        expect(screen.getByText('$20.00')).toBeInTheDocument();
      });

    it('displays an empty table when there are no sellers', () => {
        render(<PerformanceTable sellers={[]} />);

        // Verify that the table headers are rendered
        expect(screen.getByText('Month')).toBeInTheDocument();
        expect(screen.getByText('Seller Name')).toBeInTheDocument();
        expect(screen.getByText('Count of Total Orders')).toBeInTheDocument();
        expect(screen.getByText('Total Price')).toBeInTheDocument();

        // Verify that there are no rows
        const rows = screen.queryAllByRole('row');
        expect(rows).toHaveLength(1); // Only the header row should exist
    });
});
