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

const addVideo = (data) => callApi({
    url: '/api/Video/addVideo',
    method: 'post',
    data: {
        ...data
    }
})

const updateVideo = (data) => callApi({
    url: '/api/Video/updateVideo',
    method: 'put',
    data: {
        ...data
    }
})

const getVideos = (obj) => callApi({
    url: `/api/Video/getall`,
    method: 'get',
    params: {
        ...obj
    },
})

const getVideoById = (id) => callApi({
    url: `/api/Video/getVideo/${id}`,
    method: 'get',

})

const deleteVideo = (id) => callApi({
    url: `/api/Video/deleteVideo/${id}`,
    method: 'post',
})

const getQuestions = (obj) => callApi({
    url: `/api/Questions/getall`,
    method: 'get',
    params: {
        ...obj
    },
})

const getQuestionById = (id) => callApi({
    url: `/api/Questions/getQuestion/${id}`,
    method: 'get',

})

const addQuestion = (data) => callApi({
    url: '/api/Questions/addQuestion',
    method: 'post',
    data: {
        ...data
    }
})

const getAnswerByQuestionId = (id) => callApi({
    url: `/api/Answer/getAnswer/${id}`,
    method: 'get',

})

const addAnswer = (data) => callApi({
    url: '/api/Answer/addAnswer',
    method: 'post',
    data: {
        ...data
    }
})

const deleteAnswer = (id) => callApi({
    url: `/api/Answer/deleteAnswer/${id}`,
    method: 'post',
})

const updateAnswer = (data) => callApi({
    url: '/api/Answer/updateAnswer',
    method: 'put',
    data: {
        ...data
    }
})

const changeStatus = (id) => callApi({
    url: `/api/Questions/changeStatus/${id}`,
    method: 'post',
})

export default {
    changeStatus,
    updateAnswer,
    deleteAnswer,
    addAnswer,
    getAnswerByQuestionId,
    deleteVideo,
    updateVideo,
    getQuestionById,
    getVideoById,
    getVideos,
    addQuestion,
    getQuestions,
    addVideo,
    deleteArticle,
    updateArticle,
    getArticles,
    getArticle,
    postFiles,
    saveArticle,
    logout,
    login,

}