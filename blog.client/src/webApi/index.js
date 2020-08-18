import callApi from './callApi'
// import qs from 'qs'

const login = (username, password) => callApi({
    url: '/api/Users/authenticate',
    method: 'post',
    data: {
        username,
        password
    }
})

const logout = () => callApi({
    url: '/api/Users/logout',
    method: 'post',
})

const saveArticle = (data) => callApi({
    url: '/api/Article/saveArticle',
    method: 'post',
    data: {
        ...data
    }
})

const updateArticle = (data) => callApi({
    url: '/api/Article/updateArticle',
    method: 'put',
    data: {
        ...data
    }
})


const postFiles = (filesData) => callApi({
    url: `/api/File/UploadFile`,
    headers: { 'Content-Type': "multipart/form-data" },
    data: filesData,
    method: 'post'
})

const getArticle = (id) => callApi({
    url: `/api/Article/getArticle/${id}`,
    method: 'get',
})

const getArticles = (obj) => callApi({
    url: `/api/Article/getall`,
    method: 'get',
    params: {
        ...obj
    },
})

const deleteArticle = (id) => callApi({
    url: `/api/Article/deleteArticle/${id}`,
    method: 'post',
})

const saveVideo = (data) => callApi({
    url: '/api/Video/saveVideo',
    method: 'post',
    data: {
        ...data
    }
})

const getQuestions = (obj) => callApi({
    url: `/api/Questions/getall`,
    method: 'get',
    params: {
        ...obj
    },
})

const addQuestion = (data) => callApi({
    url: '/api/Questions/addQuestion',
    method: 'post',
    data: {
        ...data
    }
})

export default {
    addQuestion,
    getQuestions,
    saveVideo,
    deleteArticle,
    updateArticle,
    getArticles,
    getArticle,
    postFiles,
    saveArticle,
    logout,
    login,

}