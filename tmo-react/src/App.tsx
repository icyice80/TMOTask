import React, { useState } from 'react';
import BranchSelector from './components/BranchSelector/BranchSelector';
import PerformanceTable from './components/PerformanceTable/PerformanceTable';
import { TopPerformance } from './models';
import { getTopPerformancesByBranch } from './services';
import './App.scss';

const App: React.FC = () => {
  const [data, setData] = useState<TopPerformance[]>([]);

  const handleBranchSelect = async (branch: string) => {
      const sellers = await getTopPerformancesByBranch(branch);
      setData(sellers);
  };

  return (
    <div className="app-container">
      <h1 className="title">Monthly Top Sellers</h1>
      <BranchSelector onBranchSelect={handleBranchSelect} />
      <PerformanceTable sellers={data} />
    </div>
  );
};

export default App;