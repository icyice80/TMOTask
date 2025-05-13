import http from './httpService';

export const getBranches = async (): Promise<string[]> => {
  try {
    const response = await http.get<{ items: string[] }>('/branch/all');
    return response.data.items;
  } catch (error: any) {    
    alert('Server Error: Unable to get branches, Please try it again later');
    console.log(error);
    return [];
  }
};