import { TopPerformance } from '../../models';
import './PerformanceTable.scss';

interface PerformantsTableProps {
  sellers: TopPerformance[];
}

const PerformanceTable: React.FC<PerformantsTableProps> =  ({ sellers }: PerformantsTableProps) => (
    <div className="performance-table-wrapper">
        <table className="performance-table">
            <thead>
                <tr>
                    <th>Month</th>
                    <th>Seller Name</th>
                    <th>Count of Total Orders</th>
                    <th>Total Price</th>
                </tr>
            </thead>
            <tbody>
                {sellers.map(seller => (
                    <tr key={`${seller.month}-${seller.sellerName}`}>
                        <td>{new Date(0, seller.month - 1).toLocaleString('default', { month: 'long' })}</td>
                        <td>{seller.sellerName}</td>
                        <td>{seller.orderCount}</td>
                        <td>${seller.totalPrice.toFixed(2)}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    </div>
);

export default PerformanceTable;