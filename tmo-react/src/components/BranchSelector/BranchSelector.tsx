import React, { useEffect, useState, useRef } from 'react';
import { getBranches } from '../../services';
import './BranchSelector.scss';

interface Props {
  onBranchSelect: (branch: string) => void;
}

const BranchSelector: React.FC<Props> = ({ onBranchSelect }) => {
  const [branches, setBranches] = useState<string[]>([]);

  // Create a ref for the dropdown
  const selectRef = useRef<HTMLSelectElement>(null);

  useEffect(() => {
    getBranches().then(setBranches);
    // Focus the dropdown when the component loads
    if (selectRef.current) {
      selectRef.current.focus();
    }
  }, []);

  return (
      <div className="branch-selector">
          <select onChange={(e) => onBranchSelect(e.target.value)} defaultValue="" className="branch-select" ref={selectRef}>
              <option value="" disabled>Select a branch</option>
              {branches.map((branch) => (
                  <option key={branch} value={branch}>
                      {branch}
                  </option>
              ))}
          </select>
      </div>
  );
};

export default BranchSelector;