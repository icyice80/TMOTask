import http from './httpService';
import { TopPerformance } from '../models';

export const getTopPerformancesByBranch = async (
  branch: string
): Promise<TopPerformance[]> => {
  try {
    const response = await http.get<{ items: TopPerformance[] }>(
      `/performancereport/top-performance?branchName=${encodeURIComponent(branch)}`
    );
    return response.data.items;
  } catch (error: any) {
    if(error.status === 500){
      alert('Server Error: Unable to get Top sellers, Please try it again later');
    } else if(error.status === 409 && error.response?.data?.error){
      const err = error.response?.data?.error;
      alert(`${err.title}: ${err.message}`);
    } else if(error.status === 400){
      alert(`Please a pick Branch`);
    }

     console.log(error);
  }
  return [];
};